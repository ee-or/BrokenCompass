//Version 1.0
$Anomaly::editMode = 0;// stops the main loop form starting to the map can be safely edited

if($Anomaly::editMode){ 
   autoExec("scripts/zAnomalyGame.cs",0,0);
}


function SimObject::setPosition(%obj, %pos){
     %obj.setTransform(%pos SPC getWords(%obj.getTransform(), 3, 6));
}

datablock StaticShapeData(AGameStart){
   catagory = "misc";
   shapeFile = "flag.dts";
};
function AGameStart::onAdd(%this, %obj){  
   Parent::onAdd(%this, %obj);
   if(!isObject(StartScriptObj)){
      %obj.setName("StartScriptObj");
   }
   if(!Game.aStart && !$Anomaly::editMode){
       aGameLoop();
      Game.aStart = 0;
   }
}

function aGameLoop(){
   if(($MatchStarted + $missionRunning) == 2 && ($HostGamePlayerCount - $HostGameBotCount > 0)){
      Game.loopTime += 128;
      if(Game.loopTime > (60000 * $Anomaly::dkwUnlockTimeMin)){
         Game.unlockDarkWep = 1;
      }
      if(getRandom(1,150) == 1){
         randomSteamBlast();
      }
   }
   if(isObject(StartScriptObj)){
      schedule(128, 0, "aGameLoop");
   }
}

function steamKick(%pos, %time){
   %uppos = vectorAdd(%pos, getRandom(-100,100) SPC getRandom(-100,100) SPC 150);
   steamkick2(%pos,%time,%uppos);
}

function steamkick2(%pos,%time,%uppos){
   InitContainerRadiusSearch(%pos,  50, $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType); 
   while ((%targetObject = containerSearchNext()) != 0){
      if((%targetObject.getType() & $TypeMasks::PlayerObjectType)){
         %force = 1800;
         %tgtPos = %targetObject.getWorldBoxCenter();
         %rot = getWords(MatrixMultiply("0 0 0 0 0 1" SPC mDegToRad(getRandom(1,360)), "0 0 0 0 1 0" SPC mDegToRad(getRandom(1,45))),3,6);
         %vec = VectorNormalize(VectorSub(%uppos, %pos));
         //error(%uppos SPC %vec);
         %impulseVec = VectorScale(%vec, %force);
         %targetObject.applyImpulse(%tgtPos, %impulseVec);
      }
      else if((%targetObject.getType() &$TypeMasks::VehicleObjectType) && %targetObject.getDataBlock().getName() $= "tree19"){
         %targetObject.applyImpulse(%targetObject.getPosition(),"5000 0 1900");
      }
    } 
    if(%time > 0){
       %time -= 64;
      schedule(64,0,"steamKick2",%pos,%time,%uppos);
    }  
   
}
datablock ParticleData(SteamStackParticle) {
   dragCoefficient = "0.5";
   windCoefficient = "0";
   gravityCoefficient = "10";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "5000";
   lifetimeVarianceMS = "200";
   spinSpeed = "1";
   spinRandomMin = "-50";
   spinRandomMax = "50";
   useInvAlpha = "0";
   animateTexture = "0";
   framesPerSec = "1";
   textureCoords[0] = "0 0";
   textureCoords[1] = "0 1";
   textureCoords[2] = "1 1";
   textureCoords[3] = "1 0";
   animTexTiling = "0 0";
   textureName = "particleTest";
   colors[0] = "0.204724 0.204724 0.204724 0.99213";
   colors[1] = "0.291339 0.291339 0.291339 0.1";
   colors[2] = "0.259843 0.259843 0.259843 0.1";
   colors[3] = "0.0787402 0.0787402 0.0787402 0.015748";
   sizes[0] = "50";
   sizes[1] = "50";
   sizes[2] = "50";
   sizes[3] = "50";
   times[0] = "0";
   times[1] = "0.05";
   times[2] = "0.65";
   times[3] = "1";
};

datablock ParticleEmitterData(SteamStackEmitter) {
   ejectionPeriodMS = "10";
   periodVarianceMS = "0";
   ejectionVelocity = "500";
   velocityVariance = "100";
   ejectionOffset = "0";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "5";
   phiReferenceVel = "0";
   phiVariance = "360";
   softnessDistance = "1";
   ambientFactor = "0";
   overrideAdvance = "1";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "SteamStackParticle";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";  

};

function randomSteamBlast(){
   %time = getRandom(3000, 8000);
   if(getRandom(1,2)  == 1) %team = isObject(Team1SFX) ? 2 : 1;
   else %team = isObject(Team2SFX) ? 1 : 2;
   
   if(%team == 1 && !isObject(Team1SFX)){
      if(isObject(TreeB) && getRandom(1,2) == 1){//lols
       %pos = TreeB.position;
       %rot = TreeB.rotation;
       TreeB.delete();
       	%veh = new WheeledVehicle() {
            position = %pos;
            rotation = %rot;
            scale = "1 1 1";
            dataBlock = "tree19";
            lockCount = "0";
            homingCount = "0";
            disableMove = "0";

            Target = "126";
            mountable = "1";
            respawn = "0";
            selfPower = "1";
            lastDamagedBy = "0";
         };
         MissionCleanup.add(%veh);
         %veh.schedule(5000,"delete");
      }
      steamKick("-287.25 -10.7926 197.165",%time);
      camShake("-287.25 -10.7926 197.165");  
      %sfx = new AudioEmitter(Team1SFX) {
         position = "-287.25 -10.7926 197.165";
         rotation = "1 0 0 0";
         scale = "1 1 1";
         fileName = "fx/vehicles/htransport_boost.wav";
         useProfileDescription = "0";
         outsideAmbient = "1";
         volume = "1";
         isLooping = "1";
         is3D = "1";
         minDistance = "100";
         maxDistance = "1024";
         coneInsideAngle = "360";
         coneOutsideAngle = "360";
         coneOutsideVolume = "1";
         coneVector = "0 0 1";
         loopCount = "-1";
         minLoopGap = 0;
         maxLoopGap = 0;
         type = "EffectAudioType";

         locked = "true";
      };
      MissionCleanup.add(%sfx);
      %sfx.schedule(%time,"delete");
   	%part = new ParticleEmissionDummy() {
         position = "-325.361 10.2393 194.433";
         rotation = "0 -1 0 13";
         scale = "1 1 1";
         dataBlock = "defaultEmissionDummy";
         lockCount = "0";
         homingCount = "0";
         emitter = "SteamStackEmitter";
         velocity = "1";
      };
      %part.schedule(%time, "delete"); 
      MissionCleanup.add(%part);
      %part = new ParticleEmissionDummy() {
         position = "-325.361 -20.5607 194.433";
         rotation = "0 -1 0 13";
         scale = "1 1 1";
         dataBlock = "defaultEmissionDummy";
         lockCount = "0";
         homingCount = "0";
         emitter = "SteamStackEmitter";
         velocity = "1";
      };
      %part.schedule(%time, "delete"); 
      MissionCleanup.add(%part);
      %part = new ParticleEmissionDummy() {
         position = "-264.561 23.6393 182.433";
         rotation = "0 1 0 17.7618";
         scale = "1 1 1";
         dataBlock = "defaultEmissionDummy";
         lockCount = "0";
         homingCount = "0";
         emitter = "SteamStackEmitter";
         velocity = "1";
      };
      %part.schedule(%time, "delete"); 
      MissionCleanup.add(%part);
      %part = new ParticleEmissionDummy() {
         position = "-267.161 -30.9607 194.433";
         rotation = "0 1 0 9.99997";
         scale = "1 1 1";
         dataBlock = "defaultEmissionDummy";
         lockCount = "0";
         homingCount = "0";
         emitter = "SteamStackEmitter";
         velocity = "1";
      };   
      %part.schedule(%time, "delete"); 
      MissionCleanup.add(%part);
   }
   else if(%team == 2 && !isObject(Team2SFX)){
      if(isObject(TreeA) && getRandom(1,2) == 1){//lols
       %pos = TreeA.position;
       %rot = TreeA.rotation;
       TreeA.delete();
       	%veh = new WheeledVehicle() {
            position = %pos;
            rotation = %rot;
            scale = "1 1 1";
            dataBlock = "tree19";
            lockCount = "0";
            homingCount = "0";
            disableMove = "0";

            Target = "126";
            mountable = "1";
            respawn = "0";
            selfPower = "1";
            lastDamagedBy = "0";
         };
         MissionCleanup.add(%veh);
         %veh.schedule(5000,"delete");
      }

      steamKick("286.981 -1.84569 200",%time);
      camShake("286.981 -1.84569 200");  
      %rot[0] = "0.062007 -0.825201 0.561425 15.2465";
      %rot[1] = "-0.11061 -0.0026778 0.99386 194.328";
      %rot[2] = "-0.0999666 -0.0720035 0.992382 126.016";
      %rot[3] = "-0.115559 0.0308397 0.992822 226.772";
      %rot[4] = "-0.0770999 -0.210176 0.974619 58.7289";
      %rotation = %rot[getRandom(0,4)];
      %sfx = new AudioEmitter(Team2SFX) {
         position = "286.981 -1.84569 200"; 
         rotation = "1 0 0 0";
         scale = "1 1 1";
         fileName = "fx/vehicles/htransport_boost.wav";
         useProfileDescription = "0";
         outsideAmbient = "1";
         volume = "1";
         isLooping = "1";
         is3D = "1";
         minDistance = "100";
         maxDistance = "1024";
         coneInsideAngle = "360";
         coneOutsideAngle = "360";
         coneOutsideVolume = "1";
         coneVector = "0 0 1";
         loopCount = "-1";
         minLoopGap = 0;
         maxLoopGap = 0;
         type = "EffectAudioType";

         locked = "true";
      };
      MissionCleanup.add(%sfx);
      %sfx.schedule(%time,"delete");
      %part = new ParticleEmissionDummy() {
         position = "317.569 12.6078 187.632";
         rotation = %rotation;
         scale = "1 1 1";
         dataBlock = "defaultEmissionDummy";
         lockCount = "0";
         homingCount = "0";
         emitter = "SteamStackEmitter";
         velocity = "1";
      };
      %part.schedule(%time, "delete"); 
      MissionCleanup.add(%part);
      %part = new ParticleEmissionDummy() {
         position = "312.319 -22.988 188.805";
         rotation = %rotation;
         scale = "1 1 1";
         dataBlock = "defaultEmissionDummy";
         lockCount = "0";
         homingCount = "0";
         emitter = "SteamStackEmitter";
         velocity = "1";
      };
      %part.schedule(%time, "delete"); 
      MissionCleanup.add(%part);
      %part = new ParticleEmissionDummy() {
         position = "255.718 -21.3017 201.462";
         rotation = %rotation;
         scale = "1 1 1";
         dataBlock = "defaultEmissionDummy";
         lockCount = "0";
         homingCount = "0";
         emitter = "SteamStackEmitter";
         velocity = "1";
      };
      %part.schedule(%time, "delete"); 
      MissionCleanup.add(%part);
      %part = new ParticleEmissionDummy() {
         position = "264.561 25.2672 199.485";
         rotation = %rotation;
         scale = "1 1 1";
         dataBlock = "defaultEmissionDummy";
         lockCount = "0";
         homingCount = "0";
         emitter = "SteamStackEmitter";
         velocity = "1";
      };
      %part.schedule(%time, "delete"); 
      MissionCleanup.add(%part);
      %part = new ParticleEmissionDummy() {
         position = "277.355 -4.42543 196.623";
         rotation = %rotation;
         scale = "1 1 1";
         dataBlock = "defaultEmissionDummy";
         lockCount = "0";
         homingCount = "0";
         emitter = "SteamStackEmitter";
         velocity = "1";
      };
      %part.schedule(%time, "delete"); 
      MissionCleanup.add(%part);
   }
}
datablock ExplosionData(camShake1){
   lifeTimeMS = 10000;
   offset = 0;
   
   shakeCamera = true;
   camShakeFreq = "10.0 6.0 9.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 2;
   camShakeRadius = 200.0;
};

datablock LinearFlareProjectileData(CamProj){
   projectileShapeName = "plasmabolt.dts";
   scale               = "0.01 0.01 0.01";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   indirectDamage      = 0;
   damageRadius        = 0;
   kickBackStrength    = 0.0;
   directDamageType    = $DamageType::Explosion;
   radiusDamageType    = $DamageType::Explosion;
   Impulse = true;
   explosion           = "camShake1";

   dryVelocity       = 0.1;
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 0;
   lifetimeMS        = 128;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = -1;

   size[0]           = 0.01;
   size[1]           = 0.01;
   size[2]           = 0.01;


   numFlares         = 3;
   flareColor        = "0 1 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 1 0";
};

function camShake(%pos){
   %p = new LinearFlareProjectile() {
      dataBlock        = CamProj;
      initialDirection = "0 0 -1";
      initialPosition  = %pos;
      sourceObject     = -1; 
      sourceSlot       = 0;
      vehicleObject    = 0;
   };  
   MissionCleanup.add(%p);  
}

function replaceTrees(){
   if(!Game.rmvTrees){
      if(isObject(RandomOrganics)){
         RandomOrganics.delete();
      }
      for(%i = 0 ; %i < rmvTrees.getCount(); %i++){
         %tree = rmvTrees.getObject(%i);
         schedule(getRandom(1000,15000),0,"treeVeh",%tree); 
      }
      Game.rmvTrees = 1;
   }
}

function  treeVeh(%tree){
      %pos = %tree.position;
      %rot = %tree.rotation;
      %treeDB = %tree.DB;
      %tree.delete();
      %veh = new WheeledVehicle() {
         position = %pos;
         rotation = %rot;
         scale = "1 1 1";
         dataBlock = %treeDB;
         lockCount = "0";
         homingCount = "0";
         disableMove = "0";

         Target = "126";
         mountable = "1";
         respawn = "0";
         selfPower = "1";
         lastDamagedBy = "0";
      };
      MissionCleanup.add(%veh);
      %veh.sch = %veh.schedule(8000,"delete");  
   
}

datablock WheeledVehicleData(tree19) : ShrikeDamageProfile{
   mountable = 0;
   spawnOffset = "0 0 1.0";
   renderWhenDestroyed = false;

   catagory = "MISC";
   shapeFile = "borg19.dts";
   multipassenger = false;
   computeCRC = false;

   isShielded = false;
   explosion = BlasterExplosion;
   explosionDamage = 0.5;
   explosionRadius = 5.0;
   drag = 1.0;
   maxDamage = 1;
   destroyedLevel = 1.1;
   
   mass = 150;
   bodyFriction = 0.8;
   bodyRestitution = 0.5;
   minRollSpeed = 3;
   gyroForce = 400;
   gyroDamping = 0.3;
   stabilizerForce = 10;
   minDrag = 10;

   
   

   softSplashSoundVelocity = 10.0;
   mediumSplashSoundVelocity = 15.0;
   hardSplashSoundVelocity = 20.0;
   exitSplashSoundVelocity = 10.0;


   softImpactSpeed = 114;       
   hardImpactSpeed = 220;    

   softImpactSound = SoftImpactSound;
   hardImpactSound = HardImpactSound;

   exitingWater      = VehicleExitWaterMediumSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeMediumSplashSound;
   targetNameTag = 'Physics';
   targetTypeTag = 'Object';
   sensorData = VehiclePulseSensor;
   sensorRadius = VehiclePulseSensor.detectRadius;

   minImpactSpeed = 10;     
   speedDamageScale = 0.006;

   damageScale[$DamageType::Water] = 0;
};

function SimObject::getUpVector(%obj){
   %rot = getWords(%obj.getTransform(), 3, 6);  
   %tmat = VectorOrthoBasis(%rot);
   return getWords(%tMat, 6, 8);
}

datablock TriggerData(BoostTrigAnomaly){
   tickPeriodMS = 32;
};

function BoostTrigAnomaly::onEnterTrigger(%data, %trigger, %player){
   %mode = %trigger.mode;
   if(isObject(PZones)){
      PZones.delete();
   }
   switch(%mode){
      case 1:
         if(%trigger.ispowered()){
            %player.setPosition(%trigger.getWorldBoxCenter());
            %vel = VectorScale(VectorNormalize(%trigger.getForwardVector()), 90);
            %player.setVelocity(%vel);
            if(getSimTime() - %player.boostTrigTime > 2000){
               serverPlay3D(forceTrig, %trigger.getTransform());
               %player.client.play2D(BoostSound);
            }
            %player.boostTrigTime = getSimTime();
         }
         else{
            if(getSimTime() - %player.boostTrigMsgTime > 5000){
               messageClient(%player.client, 'MsgClient', '\c0Cannon is not powered.~wfx/powered/station_denied.wav');
            }
            %player.boostTrigMsgTime = getSimTime();
         }
         %player.lastBoostTime = getSimTime();
      case 2:
         if(%trigger.ispowered()){
            %trigPos = %trigger.getWorldBoxCenter();
            %player.setPosition(%trigPos);
            %vel = VectorScale(VectorNormalize(%trigger.getForwardVector()), 160);
            %player.setVelocity(%vel);
            serverPlay3D(BoostCannonExpSound, %trigger.getTransform());
            cannonEffect(%trigger);
         }
         else{
            messageClient(%player.client, 'MsgClient', '\c0Cannon is not powered.~wfx/powered/station_denied.wav');
         }
         %player.lastBoostTime = getSimTime();
      case 3:
         if(Game.unlockDarkWep){
            %player.setInventory(DarkWeaponX, 1, true);
            %player.setInventory(DarkAmmo, 1, true);
            %player.use(DarkWeaponX);
         }
         else{
            %minLeft = $Anomaly::dkwUnlockTimeMin - mCeil((Game.loopTime / 1000) / 60);
            %pos = 1024*5 SPC 1024*5 SPC 250;
            %plrPos = %player.getPosition();
            %p = new SniperProjectile() {
               dataBlock        =  MOACShot;
               initialDirection = vectorNormalize(vectorSub(%plrPos, %pos));
               initialPosition  = %pos;
               sourceObject     = -1;
               damageFactor     = 2;
               sourceSlot       = "";
               sObj = %obj;
            };
            %p.setEnergyPercentage(1);
            MissionCleanup.add(%p);
            messageClient(%player.client, 'MsgClient', '\c0The dark weapon unlocks in %1 minutes.~wfx/powered/station_denied.wav', %minLeft);
         }
      default:
         return;
   }
}
function cannonEffect(%trigger){
       %p = new LinearFlareProjectile() {
         dataBlock        = BoostCannonEffect;
         initialDirection = vectorScale(%trigger.getForwardVector(),-1);
         initialPosition  = vectorAdd(%trigger.getWorldBoxCenter(),vectorScale(%trigger.getForwardVector(),8));
         sourceObject     = -1;
         sourceSlot       = 0;
         vehicleObject    = 0;
      };
      MissionCleanup.add(%p);
}

function BoostTrigAnomaly::onTickTrigger(%this, %triggerId){
 // anti spam
}
function BoostTrigAnomaly::onleaveTrigger(%data, %trigger, %player){

}

datablock ForceFieldBareData(APlrCannonBlocker)
{
   fadeMS           = 1000;
   baseTranslucency = 0.01;
   powerOffTranslucency = 0.0;
   teamPermiable    = true;
   otherPermiable   = false;
   color            = "0.28 0.89 0.31";
   powerOffColor    = "0.0 0.0 0.0";
   targetTypeTag    = 'ForceField'; 

   texture[0] = "skins/forcef1";
   texture[1] = "skins/forcef2";
   texture[2] = "skins/forcef3";
   texture[3] = "skins/forcef4";
   texture[4] = "skins/forcef5";

   framesPerSec = 10;
   numFrames = 5;
   scrollSpeed = 15;
   umapping = 1.0;
   vmapping = 0.15;
};




datablock AudioProfile(TeleporterAStart){
   filename    = "fx/misc/nexus_cap.wav";
   description = AudioDefault3d;
   preload = true;
};


datablock StaticShapeData(TeleporterA){
   catagory = "Teleporters";
   shapefile = "station_teleport.dts";
   mass = 10;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   targetNameTag = '';
   targetTypeTag = 'Teleporter';
//----------------------------------
   maxDamage = 1.00;
   destroyedLevel = 1.00;
   disabledLevel = 0.70;
   explosion      = ShapeExplosion;
   expDmgRadius = 8.0;
   expDamage = 0.4;
   expImpulse = 1500.0;
   // don't allow this object to be damaged in non-team-based
   // mission types (DM, Rabbit, Bounty, Hunters)
   noIndividualDamage = true;

   dynamicType = $TypeMasks::StationObjectType;
   isShielded = true;
   energyPerDamagePoint = 75;
   maxEnergy = 50;
   rechargeRate = 0.35;
   doesRepair = true;
   humSound = StationInventoryHumSound;

   cmdCategory = "Support";
   cmdIcon = CMDStationIcon;
   cmdMiniIconName = "commander/MiniIcons/com_inventory_grey";

   debrisShapeName = "debris_generic.dts";
   debris = StationDebris;
};

//datablock Staticshapedata(teledestroyed) : teleporter
//{
   //shapefile = "station_teleport.dts";
//};

$playerreject = 6;
function TeleporterA::onDestroyed(%data, %obj, %prevState){
   //set the animations
   %obj.playThread(1, "transition");
   %obj.setThreadDir(1, true);
   %obj.setDamageState(Destroyed);
   //%obj.setDatablock(teledestroyed);
   %obj.getDataBlock().onLosePowerDisabled(%obj);
}
function TeleporterA::damageObject(%data, %targetObject, %sourceObject, %position, %amount, %damageType){
   if( %targetObject.invincible)
		return; 
   parent::damageObject(%data, %targetObject, %sourceObject, %position, %amount, %damageType);
}
//---this is where I create the triggers and put them right over the nexus base's
function TeleporterA::onEnabled(%data, %obj, %prevState){ 
   %level = %obj.getdamagelevel();
  %obj.setdamagelevel(%level);
   if(%obj.ispowered())
   {
      %obj.playthread(1, "transition");
      %obj.setThreadDir(1, false);
      %obj.playThread(0, "ambient");
      %obj.setThreadDir(0, true);
   }
   else
   {
      %obj.playThread(0, "transition");
      %obj.setThreadDir(0, false);
   }
  Parent::onEnabled(%data, %obj, %prevState);
}

function TeleporterA::gainPower(%data, %obj){
   //%obj.setDatablock(teleporter);
   Parent::gainPower(%data, %obj);
   %obj.playthread(1, "transition");
   %obj.setThreadDir(1, false);
   %obj.playThread(0, "ambient");
   %obj.setThreadDir(0, true);
}

function TeleporterA::losePower(%data, %obj){
   %obj.playThread(0, "transition");
   %obj.setThreadDir(0, false);
   Parent::losePower(%data, %obj);
}

function TeleporterA::onAdd(%this, %tp){
   Parent::onAdd(%this, %tp);
   if(!isObject(tpSimSet)){
      new simSet(tpSimSet);
      MissionCleanup.add(tpSimSet);
   }
   tpSimSet.add(%tp);
   
   %trigger = new Trigger()
   {
      dataBlock = NewTeleportATrigger;
      polyhedron = "-0.75 0.75 0.1 1.5 0.0 0.0 0.0 -1.5 0.0 0.0 0.0 2.3";
   };
   
   MissionCleanup.add(%trigger);
   if(%tp.noflag $= "")
      %tp.noflag = "0";
   if(%tp.oneway $= "")
      %tp.oneway = "0";
   if(%tp.linkID $= "")
      %tp.linkID = "0";
   if(%tp.linkTo $= "")
      %tp.linkTo = "0";
   if(%tp.invincible $= ""){
      %tp.invincible = 1;
   }
   if(%tp.teamOnly $= ""){
      %tp.teamOnly = 1;
   }

   %trigger.setTransform(%tp.getTransform());
   
   %trigger.sourcebase = %tp;
   %tp.trigger = %trigger;

 //--------------do we need power?-----------------------
   %tp.playThread(1, "ambient");
   %tp.playThread(0, "transition");
   %tp.playThread(0, "ambient");

   %pos = %trigger.position;

}


datablock TriggerData(NewTeleportATrigger){
   tickPeriodMS =  256;
};


function NewTeleportATrigger::onEnterTrigger(%data, %trigger, %player)
{
   %colObj = %player;
   %client = %player.client;

   if(%player.transported $= "1")  // if this player was just transported
   {
      %player.transported = "0";
      %colObj.setMoveState(false);
      %trigger.player = %player;
      return; // then get out or it will never stop
   }

//--------------do we have power?-----------------------
   if(%trigger.sourcebase.ispowered() == 0){
      messageClient(%player.client, 'MsgClient', '\c0Teleporter is not powered.~wfx/powered/station_denied.wav');
      return;
   }

//----------------------disabled?-----------------------
   if(%trigger.sourcebase.isDisabled()){
      messageClient(%colObj.client, 'msgStationDisabled', '\c2Teleporter is disabled.~wfx/powered/station_denied.wav');
      return;
   }

//--------------are we on the right team?-----------------------
   if(%player.team != %trigger.sourcebase.team && %trigger.sourcebase.teamOnly){
      messageClient(%player.client, 'MsgClient', '\c0Wrong team.~wfx/powered/station_denied.wav');
      return;
   }

   //------------are we teleporting?-----------------------
   if(isObject(%trigger.player)){
      messageClient(%player.client, 'MsgClient', '\c0Teleporter in use.~wfx/powered/station_denied.wav');
      return;
   }
   //-------------is this a oneway teleporter?------------------------
   if(%trigger.sourcebase.oneway == "1"){
      messageClient(%player.client, 'MsgLeaveMissionArea', '\c1This teleporter is oneway only.~wfx/powered/station_denied.wav');
      return;
   }

   //-------------are we teleporting with flag?----------------------------------------
   %flag = %player.holdingflag;
   if(%player.holdingFlag > 0){
      if(%trigger.sourcebase.noflag $= "1"){
         if(%flag.team == 1)
            %otherTeam = 2;
         else
            %otherTeam = 1;

         //game.flagReset(%player.holdingflag);
         Game.dropFlag(%player); 
         messageClient(%player.client, 'MsgClient', '\c0Cant teleport with flag');
         //messageTeam(%flag.team, 'MsgCTFFlagReturned', '\c2Your flag was returned.~wfx/misc/flag_return.wav', 0, 0, %flag.team);
         //messageTeam(0, 'MsgCTFFlagReturned', '\c2The %2 flag was returned to base.~wfx/misc/flag_return.wav', 0, $teamName[%flag.team], %flag.team);
      }
   }
   %destList = getDestTeleA(%trigger.sourcebase,%player.client);
   
   if(%destList != -1){
      %vc = 0;
      for(%x = 0; %x < getFieldCount(%destList); %x++){
         %targetObj = getField(%destList,%x);
         // make sure its not in use  and its not destroyed  and it has power 
         if(!isObject(%targetObj.trigger.player) && %targetObj.isEnabled() && %targetObj.isPowered())
            %validTarget[%vc++] = %targetObj;
         else
            %inValidTarget[%ivc++] = %targetObj;
         
      }
      if(!%vc){
         if(isObject(%inValidTarget[1].trigger.player))
            messageClient(%player.client, 'MsgClient', '\c0Destination teleporter in use.~wfx/powered/station_denied.wav');
         else if(!%inValidTarget[1].isEnabled())
            messageClient(%player.client, 'MsgClient', '\c0Destination teleporter is destroyed.~wfx/powered/station_denied.wav');
         else if(!%inValidTarget[1].isPowered())
            messageClient(%player.client, 'MsgClient', '\c0Destination teleporter lost power.~wfx/powered/station_denied.wav');
         else
            messageClient(%player.client, 'MsgClient', '\c0Destination teleporter in use, destroyed, or loss power.~wfx/powered/station_denied.wav');
      }
      else{
         %dest = %validTarget[getRandom(1,%vc)];
         serverPlay3D(TeleporterAStart, %trigger.getTransform());
         messageClient(%player.client, 'MsgClient', '~wfx/misc/nexus_cap.wav');  
         %player.transported = 1;
         %teleDest =  vectorAdd(%dest.getPosition(),"0 0 0.5");
         teleporteffect(vectorAdd(%trigger.sourcebase.getPosition(),"0 0 0.5"));
         teleporteffect(%teleDest);
         %player.setmovestate(true);
         %player.setTransform(vectorAdd(%trigger.sourcebase.getPosition(),"0 0 0.5") SPC getWords(%player.getTransform(),3,6));
         %player.startfade(500,0,true);
         %player.schedule(500, "settransform", %teleDest SPC getWords(%player.getTransform(),3,6));
         %player.schedule(500, "startfade", 500, 0, false);
         %player.schedule(500, "setmovestate", false);
      }
   }
   else
      messageClient(%player.client, 'MsgLeaveMissionArea', '\c1This teleporter has no destination.~wfx/misc/warning_beep.wav');   
}
function getDestTeleA(%obj,%client){
   %idCount = getFieldCount(%obj.linkTo);
   if(!%idCount || %obj.team != %client.team)
      return -1;
   %count = 0;
   for(%i = 0; %i < tpSimSet.getCount(); %i++){
      %dest = tpSimSet.getObject(%i);
      if(%dest.team == %client.team && %dest != %obj){
         for(%a = 0; %a <  getFieldCount(%dest.linkTo); %a++){
            %destID = getField(%dest.linkTo,%a);
            if(%obj.linkID == %destID){// see if it links back to us
               if(%count++ == 1)
                  %teleList = %dest;
               else
                  %teleList = %teleList TAB %dest;
            }
         }
      }
   }
   if(%count > 0){
      return %teleList;
   }
   return -1;
}

function NewTeleportATrigger::onleaveTrigger(%data, %trigger, %player){
   if(%player == %trigger.player){
      %trigger.player = 0;  
   }
   if(!%player.transported){
      %player.tpWarn  = 0;
      %player.tpTime = 0;
      %player.tpDmgTime = 0;
   }
}

function NewTeleportATrigger::onTickTrigger(%data, %trig){
   %player = %trig.player; 
   if(isObject(%player)){
     if(%player.getState() $= "Dead"){
        %player.blowUp();
        %trig.player = 0;
     }
     else{
         if(%player.tpTime > 3000 && !%player.tpWarn){
            messageClient(%player.client, 'MsgLeaveMissionArea', '\c1Move off the teleporter or take damage.~wfx/misc/warning_beep.wav');
            %player.tpWarn = 1;         
         }
         %player.tpTime += %data.tickPeriodMS;
         if(%player.tpTime > 3000){
            %player.tpDmgTime += %data.tickPeriodMS;
            if(%player.tpDmgTime > 1000){
               %player.setdamageflash(0.3);
               %player.damage(0, %player.getPosition(), 0.04, $DamageType::Explosion);
            }
         }
     }
   }
   else
      %trig.player = 0;
}

function teleporteffect(%position){   
   %effect1 = new ParticleEmissionDummy(){
      position = %position;
      rotation = "1 0 0 0";
      scale = "1 1 1";
      dataBlock = "doubleTimeEmissionDummy";
      emitter = "AABulletExplosionEmitter2";
      velocity = "1";
   };

   %effect2 = new ParticleEmissionDummy(){
      position = getWord(%position,0) SPC getWord(%position,1) SPC getWord(%position,2) + 0.5;
      rotation = "1 0 0 0";
      scale = "1 1 1";
      dataBlock = "doubleTimeEmissionDummy";
      emitter = "AABulletExplosionEmitter2";
      velocity = "1";
   };

   %effect3 = new ParticleEmissionDummy(){
      position = getWord(%position,0) SPC getWord(%position,1) SPC getWord(%position,2) + 1;
      rotation = "1 0 0 0";
      scale = "1 1 1";
      dataBlock = "doubleTimeEmissionDummy";
      emitter = "AABulletExplosionEmitter2";
      velocity = "1";
   };

   %effect4 = new ParticleEmissionDummy(){
      position = getWord(%position,0) SPC getWord(%position,1) SPC getWord(%position,2) + 1.5;
      rotation = "1 0 0 0";
      scale = "1 1 1";
      dataBlock = "doubleTimeEmissionDummy";
      emitter = "AABulletExplosionEmitter2";
      velocity = "1";
   };
   MissionCleanup.add(%effect1);
   MissionCleanup.add(%effect2);
   MissionCleanup.add(%effect3);
   MissionCleanup.add(%effect4);
   %effect1.schedule(2000, "delete");
   %effect2.schedule(2000, "delete");
   %effect3.schedule(2000, "delete");
   %effect4.schedule(2000, "delete");
}

function SimObject::getUpVector(%obj){
   %rot = getWords(%obj.getTransform(), 3, 6);  
   %tmat = VectorOrthoBasis(%rot);
   return getWords(%tMat, 6, 8);
}