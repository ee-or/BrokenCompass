
datablock StaticShapeData(dnTest)
{
   catagory             = "misc";
   shapeFile            = "dsPlane2.dts";
   alwaysAmbient = true;
};
datablock StaticShapeData(dnTestSnow)
{
   catagory             = "misc";
   shapeFile            = "dsPlane3.dts";
   alwaysAmbient = true;
};
datablock StaticShapeData(dsFlameEffect)
{
   catagory             = "misc";
   shapeFile            = "dsFlame.dts";
   alwaysAmbient = true;
};

function dnTest::onAdd(%this, %obj){ 
   parent::onAdd(%this,%obj);

   if(!Game.startFlagCheck){
      schedule(512, 0, "carrierFlagCheck");
      Game.startFlagCheck =1;
      if(Game.class $= "LCTFGame"){
         activatePackage(carrierLCTF);
      }
      else{
         schedule(512, 0, "cleanupCarrierCTFObjs");
      }
   }
}

function dnTest::onRemove(%this, %obj){
   if (isActivePackage(carrierLCTF)){
      deactivatePackage(carrierLCTF);
   }
}
 

function dnTestSnow::onAdd(%this, %obj){ 
   parent::onAdd(%this,%obj);

   if(!Game.startFlagCheck){
      schedule(512, 0, "carrierFlagCheck");
      Game.startFlagCheck =1;
      if(Game.class $= "LCTFGame"){
         activatePackage(carrierLCTF);
      }
      else{
         schedule(512, 0, "cleanupCarrierCTFObjs");
      }
   }
}

function dnTestSnow::onRemove(%this, %obj){
   if (isActivePackage(carrierLCTF)){
      deactivatePackage(carrierLCTF);
   }
}


package carrierLCTF{
   function deleteNonLCTFObjects(){
      schedule(128, 0, "deleteLCTFObj");
   } 
};

function cleanupCarrierCTFObjs(){
   for(%i = 0; %i < MissionCleanUp.getCount(); %i++){
      %obj = MissionCleanUp.getObject(%i);   
      if(%obj.getClassName() $= "Trigger"){
         if(%obj.getDatablock().getName() $= "stationTrigger" && %obj.station.getDataBlock().getName() $= "StationVehicle"){
            %obj.schedule(32, "delete");
         } 
      }
   }
}

function deleteLCTFObj(){
   %c = 0;
   InitContainerRadiusSearch("0 0 0", 9999, $TypeMasks::ItemObjectType | $TypeMasks::TurretObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::StaticShapeObjectType);
   while ((%obj = containerSearchNext()) != 0){
      if(%obj.Datablock !$= "flag" && 
      %obj.Datablock !$= "RepairKit" && 
      %obj.Datablock !$= "RepairPatch" && 
      %obj.Datablock !$= "ExteriorFlagStand" && 
      %obj.Datablock !$= "InteriorFlagStand" && 
      %obj.Datablock !$= "NexusBase" &&
      %obj.Datablock !$= "SensorMediumPulse" &&
      %obj.Datablock !$= "SensorLargePulse" &&
      %obj.Datablock !$= "dnTestSnow" &&
      %obj.Datablock !$= "dnTest" &&
      %obj.Datablock !$= "StationVehicle" &&
      %obj.Datablock !$= "StationVehiclePad"){
         %deleteList[%c] = %obj;
         %c++;
      }
   }

   for(%i = 0; %i < MissionCleanUp.getCount(); %i++){
      %obj = MissionCleanUp.getObject(%i);   
      if(%obj.getClassName() $= "Trigger"){
         if(%obj.getDatablock().getName() $= "stationTrigger"){
            %deleteList[%c] = %obj;
            %c++;
         }
      }
   }

   for(%i = 0; %i  < %c; %i++){
         %deleteList[%i].delete();
   }
}


function carrierFlagCheck(){
   //error("test" SPC getWord($TeamFlag[2].getPosition(),2));
    if(!isObject($TeamFlag[1].carrier) && !$TeamFlag[1].isHome){
      %z = getWord($TeamFlag[1].getPosition(),2);
      if(%z < 105 && !isEventPending($TeamFlag[1].lavaEnterThread)){ 
         $TeamFlag[1].lavaEnterThread = Game.schedule(2000, "flagReturn", $TeamFlag[1]);
         $TeamFlag[1].setVelocity("-150 0 0");
      }
   }

   if(!isObject($TeamFlag[2].carrier) && !$TeamFlag[2].isHome){
      %z = getWord($TeamFlag[2].getPosition(),2);
      if(%z < 105 && !isEventPending($TeamFlag[2].lavaEnterThread)){ 
         $TeamFlag[2].lavaEnterThread = Game.schedule(2000, "flagReturn", $TeamFlag[2]);
         $TeamFlag[2].setVelocity("-150 0 0");
      }
   }
   spawnFlyByObjs();
   if(Game.startFlagCheck){
      $carrierTimer = schedule(256, 0, "carrierFlagCheck");
   }
}

function dsFlameEffect::onRemove(%this, %Obj){
   Parent::onRemove();
   cancel($carrierTimer);// end the sim 
}

datablock ParticleData(FlameExParticle) { 
   dragCoefficient = "0";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "200";
   lifetimeVarianceMS = "0";
   spinSpeed = "1";
   spinRandomMin = "-80";
   spinRandomMax = "80";
   useInvAlpha = "0";
   framesPerSec = "1";
   textureName = "particleTest";
   colors[0] = "0 0 1 1";
   colors[1] = "1 0.3 0 0.25";
   colors[2] = "1 0.3 0 0.15";
   colors[3] = "1 0.3 0 0";
   sizes[0] = "4";
   sizes[1] = "6";
   sizes[2] = "8";
   sizes[3] = "12";
   times[0] = "0.1";
   times[1] = "0.2";
   times[2] = "0.8";
   times[3] = "1";
};

datablock ParticleEmitterData(FlameExEmitter) {
   ejectionPeriodMS = "4";
   periodVarianceMS = "0";
   ejectionVelocity = "150";
   velocityVariance = "2";
   ejectionOffset = "4";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "20";
   phiReferenceVel = "0";
   phiVariance = "360";
   softnessDistance = "1";
   ambientFactor = "0";
   overrideAdvance = "1";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "FlameExParticle";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";
   
   alignParticles = "0";
   alignDirection = "0 1 0";   
};

datablock ParticleData(DesertDustParticle) {
   dragCoefficient = "0";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "4000";
   lifetimeVarianceMS = "200";
   spinSpeed = "1";
   spinRandomMin = "0";
   spinRandomMax = "0";
   useInvAlpha = "1";
   textureName = "dsDust";
   colors[0] = "0.721569 0.792157 0.713725 0.25";
   colors[1] = "0.721569 0.792157 0.713725 0.25";
   colors[2] = "0.721569 0.792157 0.713725 0.25";
   colors[3] = "0.0787402 0.0787402 0.0787402 0.0";
   sizes[0] = "40";
   sizes[1] = "150";
   sizes[2] = "150";
   sizes[3] = "150";
   times[0] = "0.1";
   times[1] = "0.2"; 
   times[2] = "0.75";
   times[3] = "1";
};


datablock ParticleEmitterData(DesertDustEmitter) {
   ejectionPeriodMS = "22";
   periodVarianceMS = "0";
   ejectionVelocity = "140";
   velocityVariance = "50";
   ejectionOffset = "1";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "15";
   phiReferenceVel = "0";
   phiVariance = "360";
   softnessDistance = "1";
   ambientFactor = "0";
   overrideAdvance = "1";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "DesertDustParticle";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";
   reverseOrder = "0";
   alignParticles = "0";
   alignDirection = "0 1 0";
   highResOnly = "1";
};

datablock ParticleData(SnowDustParticle) {
   dragCoefficient = "0";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "4000";
   lifetimeVarianceMS = "200";
   spinSpeed = "1";
   spinRandomMin = "0";
   spinRandomMax = "0";
   useInvAlpha = "1";
   textureName = "smoke02";
   colors[0] = "1 1 1 0.25";
   colors[1] = "1 1 1 0.25";
   colors[2] = "1 1 1 0.25";
   colors[3] = "1 1 1 0.0";
   sizes[0] = "40";
   sizes[1] = "150";
   sizes[2] = "150";
   sizes[3] = "150";
   times[0] = "0.1";
   times[1] = "0.2"; 
   times[2] = "0.75";
   times[3] = "1";
};


datablock ParticleEmitterData(SnowDustEmitter) {
   ejectionPeriodMS = "22";
   periodVarianceMS = "0";
   ejectionVelocity = "140";
   velocityVariance = "50";
   ejectionOffset = "1";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "15";
   phiReferenceVel = "0";
   phiVariance = "360";
   softnessDistance = "1";
   ambientFactor = "0";
   overrideAdvance = "1";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "SnowDustParticle";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";
   reverseOrder = "0";
   alignParticles = "0";
   alignDirection = "0 1 0";
   highResOnly = "1";
};

datablock ParticleData(RedCoreParticle){
   windCoefficient      = 0.0;
   dragCoefficient      = 0.1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 2000;
   lifetimeVarianceMS   = 0;
   useInvAlpha          = false;
   spinRandomMin        = -90.0;
   spinRandomMax        = 50.0;
   textureName          = "Special/crescent4";
   colors[0]     = "1.0 0.0 0.0 1.0";  // 0 0.3 0.9 1.0
   colors[1]     = "1.0 0.0 0.0 1.0";  // 0.0 0.3 0.9 0.2
   colors[2]     = "0.0 0.0 0.0 0.0";  // 0 0.3 0.9 0.0
   sizes[0]      = 5.0;
   sizes[1]      = 5.0;
   sizes[2]      = 5.0;
   times[0]      = 0.0;
   times[1]      = 0.8;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(RedCoreEmitter){
   ejectionPeriodMS = 15;
   periodVarianceMS = 0;
   ejectionVelocity = 1.01;
   velocityVariance = 0.0;
   ejectionOffset   = 2;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 1;
   particles = "RedCoreParticle";
};

datablock ParticleData(DarkCoreParticle){
   windCoefficient      = 0.0;
   dragCoefficient      = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = -3;
   lifetimeMS           = 1500;
   lifetimeVarianceMS   = 000;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 50.0;
   textureName          = "particleTest";
   colors[0]     = "0.0 0.0 0.0 0.0";  // 0 0.3 0.9 1.0
   colors[1]     = "0.0 0.0 0.0 1.0";  // 0.0 0.3 0.9 0.2
   colors[2]     = "0.0 0.0 0.0 0.0";  // 0 0.3 0.9 0.0
   sizes[0]      = 1.0;
   sizes[1]      = 1.0;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(DarkCoreEmitter){
   ejectionPeriodMS = 6;
   periodVarianceMS = 0;
   ejectionVelocity = 1.01;
   velocityVariance = 0.0;
   ejectionOffset   = 1.5;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 1;
   particles = "DarkCoreParticle";
};

datablock ParticleData(SnowParticle3) {
   dragCoefficient = "0";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "4000";
   lifetimeVarianceMS = "200";
   spinSpeed = "1";
   spinRandomMin = "-50";
   spinRandomMax = "50";
   useInvAlpha = "0";   
   textureName = "precipitation/snowflake002";
   colors[0] = "0.204724 0.204724 0.204724 0.5";
   colors[1] = "0.291339 0.291339 0.291339 1";
   colors[2] = "0.259843 0.259843 0.259843 1";
   colors[3] = "0.0787402 0.0787402 0.0787402 1";
   sizes[0] = "0.5";
   sizes[1] = "0.5";
   sizes[2] = "0.5";
   sizes[3] = "0.5";
   times[0] = "0.1";
   times[1] = "0.2";
   times[2] = "0.9";
   times[3] = "1";
};

datablock ParticleEmitterData(SnowEmitter3) {
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "100";
   velocityVariance = "50";
   ejectionOffset = "40";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "30";
   phiReferenceVel = "0";
   phiVariance = "360";
   softnessDistance = "1";
   ambientFactor = "0";
   overrideAdvance = "1";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "SnowParticle3";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";
   reverseOrder = "0";
   alignParticles = "0";
   alignDirection = "0 1 0";
   highResOnly = "1";
};


$orgSpeed = 190;

datablock LinearProjectileData(orgProj0)
{
   projectileShapeName = "snowRockSpire.dts";
   emitterDelay        = -1;
   directDamage        = 1.0;
   hasDamageRadius     = false;
   indirectDamage      = 0.50;
   damageRadius        = 7.5;
   radiusDamageType    = $DamageType::impact;
   kickBackStrength    = 1;  // z0dd - ZOD, 4/24/02. Was 1750

   sound 				= "";
   explosion           = "grenadeExplosion";
   underwaterExplosion = "UnderwaterDiscExplosion";
   splash              = DiscSplash;

   dryVelocity       = $orgSpeed; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 90
   wetVelocity       = 55; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 50
   velInheritFactor  = 1; // z0dd - ZOD, 3/30/02. was 0.5
   fizzleTimeMS      = 9000;
   lifetimeMS        = 12000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 20.0; // z0dd - ZOD, 4/24/02. Was 0.0. 20 degrees skips off water
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;

   hasLight    = false;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";
};


datablock LinearProjectileData(orgProj1)
{
   projectileShapeName = "sorg22.dts";
   emitterDelay        = -1;
   directDamage        = 1.0;
   hasDamageRadius     = false;
   indirectDamage      = 0.50;
   damageRadius        = 7.5;
   radiusDamageType    = $DamageType::impact;
   kickBackStrength    = 1;  // z0dd - ZOD, 4/24/02. Was 1750

   sound 				= "";
   explosion           = "grenadeExplosion";
   underwaterExplosion = "UnderwaterDiscExplosion";
   splash              = DiscSplash;

   dryVelocity       = $orgSpeed; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 90
   wetVelocity       = 55; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 50
   velInheritFactor  = 1; // z0dd - ZOD, 3/30/02. was 0.5
   fizzleTimeMS      = 9000;
   lifetimeMS        = 12000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 20.0; // z0dd - ZOD, 4/24/02. Was 0.0. 20 degrees skips off water
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;

   hasLight    = false;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";
};


datablock LinearProjectileData(orgProj2)
{
   projectileShapeName = "sorg20.dts";
   emitterDelay        = -1;
   directDamage        = 1.0;
   hasDamageRadius     = false;
   indirectDamage      = 0.50;
   damageRadius        = 7.5;
   radiusDamageType    = $DamageType::impact;
   kickBackStrength    = 1;  // z0dd - ZOD, 4/24/02. Was 1750

   sound 				= "";
   explosion           = "grenadeExplosion";
   underwaterExplosion = "UnderwaterDiscExplosion";
   splash              = DiscSplash;

   dryVelocity       = $orgSpeed; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 90
   wetVelocity       = 55; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 50
   velInheritFactor  = 1; // z0dd - ZOD, 3/30/02. was 0.5
   fizzleTimeMS      = 9000;
   lifetimeMS        = 12000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 20.0; // z0dd - ZOD, 4/24/02. Was 0.0. 20 degrees skips off water
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;

   hasLight    = false;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";
};


datablock LinearProjectileData(orgProj3)
{
   projectileShapeName = "xorg5.dts";
   emitterDelay        = -1;
   directDamage        = 1.0;
   hasDamageRadius     = false;
   indirectDamage      = 0.50;
   damageRadius        = 7.5;
   radiusDamageType    = $DamageType::impact;
   kickBackStrength    = 1;  // z0dd - ZOD, 4/24/02. Was 1750

   sound 				= "";
   explosion           = "grenadeExplosion";
   underwaterExplosion = "UnderwaterDiscExplosion";
   splash              = DiscSplash;

   dryVelocity       = $orgSpeed; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 90
   wetVelocity       = 55; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 50
   velInheritFactor  = 1; // z0dd - ZOD, 3/30/02. was 0.5
   fizzleTimeMS      = 9000;
   lifetimeMS        = 12000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 20.0; // z0dd - ZOD, 4/24/02. Was 0.0. 20 degrees skips off water
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;

   hasLight    = false;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";
};

datablock LinearProjectileData(orgProj4)
{
   projectileShapeName = "porg6.dts";
   emitterDelay        = -1;
   directDamage        = 1.0;
   hasDamageRadius     = false;
   indirectDamage      = 0.50;
   damageRadius        = 7.5;
   radiusDamageType    = $DamageType::impact;
   kickBackStrength    = 1;  // z0dd - ZOD, 4/24/02. Was 1750

   sound 				= "";
   explosion           = "grenadeExplosion";
   underwaterExplosion = "UnderwaterDiscExplosion";
   splash              = DiscSplash;

   dryVelocity       = $orgSpeed; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 90
   wetVelocity       = 55; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 50
   velInheritFactor  = 1; // z0dd - ZOD, 3/30/02. was 0.5
   fizzleTimeMS      = 9000;
   lifetimeMS        = 12000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 20.0; // z0dd - ZOD, 4/24/02. Was 0.0. 20 degrees skips off water
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;

   hasLight    = false;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";
};

datablock LinearProjectileData(orgProj5)
{
   projectileShapeName = "desSpire.dts";
   emitterDelay        = -1;
   directDamage        = 1.0;
   hasDamageRadius     = false;
   indirectDamage      = 0.50;
   damageRadius        = 7.5;
   radiusDamageType    = $DamageType::impact;
   kickBackStrength    = 1;  // z0dd - ZOD, 4/24/02. Was 1750

   sound 				= "";
   explosion           = "grenadeExplosion";
   underwaterExplosion = "UnderwaterDiscExplosion";
   splash              = DiscSplash;

   dryVelocity       = $orgSpeed; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 90
   wetVelocity       = 55; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 50
   velInheritFactor  = 1; // z0dd - ZOD, 3/30/02. was 0.5
   fizzleTimeMS      = 9000;
   lifetimeMS        = 12000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 20.0; // z0dd - ZOD, 4/24/02. Was 0.0. 20 degrees skips off water
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;

   hasLight    = false;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";
};


function spawnFlyByObjs(){ 
    %amount = MissionGroup.musicTrack $= "ice" ? 4 : 2;
   if(($MatchStarted + $missionRunning) == 2 && (Game.rngOrg++ % %amount == 0)){
      
      %rngMax = MissionGroup.musicTrack $= "ice" ? 2 : getRandom(3,5);
      %rngMin = MissionGroup.musicTrack !$= "ice";
      %p = new LinearProjectile() {
         dataBlock        = orgProj @ getRandom(%rngMin, %rngMax);
         initialDirection = "-1 0 0";
         initialPosition  = vectorAdd("702 0 101", 0 SPC getRandom(-700, 700) SPC 0);
         sourceObject     = -1;
         sourceSlot       = 0;
         vehicleObject    = 0; 
      };
      MissionCleanup.add(%p); 
   }
}