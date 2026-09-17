datablock ForceFieldBareData(FirnAllFastField)
{
   fadeMS           = 1000;
   baseTranslucency = 0.30;
   powerOffTranslucency = 0.0;
   teamPermiable    = true;
   otherPermiable   = true;
   color            = "0.0 0.0 1.0";   
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

function FirnAllFastField::onAdd(%data, %obj){
   parent::onAdd(%data,%obj);
   if(%obj.pz.getClassName() $= "PhysicalZone"){
		%obj.pz.delete(); 
   }
}

function SimObject::getUpVectorFirn(%obj){
   %rot = getWords(%obj.getTransform(), 3, 6);  
   %tmat = VectorOrthoBasis(%rot);
   return getWords(%tMat, 6, 8);
}
datablock TriggerData(BoostTrigFirn){
   tickPeriodMS = 32;
};

function BoostTrigFirn::onEnterTrigger(%data, %trigger, %player){

   if(!Game.firnSet){
      Game.firnSet = 1;
      seeA.setScopeAlways();
      seeB.setScopeAlways();
      seeC.setScopeAlways();
   }

   if(%trigger.mode == 1){
      %player.setPosition(%trigger.getWorldBoxCenter());
      %vel = VectorScale(VectorNormalize(%trigger.getUpVectorFirn()), 100);
      %player.setVelocity(%vel);
      if(getSimTime() - %player.boostTrigTime > 2000){
         serverPlay3D(forceTrig, %trigger.getTransform());
         %player.client.play2D(BoostSound);
      }
      %player.boostTrigTime = getSimTime();
   }
   else if(%trigger.mode == 2){
      %trigPos = %trigger.getWorldBoxCenter();
      %player.setPosition(%trigPos);
      %vel = VectorScale(VectorNormalize(%trigger.getUpVectorFirn()), 100);
      %player.setVelocity(%vel);
      serverPlay3D(BoostCannonExpSound, %trigger.getTransform());
      %p = new LinearFlareProjectile() {
         dataBlock        = BoostCannonEffect;
         initialDirection = %trigger.getUpVectorFirn();
         initialPosition  = %trigPos;
         sourceObject     = %player;
         sourceSlot       = 0;
         vehicleObject    = 0;
      };
      MissionCleanup.add(%p);
}
}
function BoostTrigFirn::onTickTrigger(%this, %triggerId){
 // anti spam
}


datablock ParticleData(SnowMistParticle) {
   dragCoefficient = "0";
   windCoefficient = "-1";
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
   colors[0] = "0.3 0.3 0.3 0.0";
   colors[1] = "0.3 0.3 0.3 1";
   colors[2] = "0.3 0.3 0.3 1";
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

datablock ParticleEmitterData(SnowMistEmitter) {
   ejectionPeriodMS = "12";
   periodVarianceMS = "0";
   ejectionVelocity = "140";
   velocityVariance = "50";
   ejectionOffset = "50";
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
   particles = "SnowMistParticle";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";
   reverseOrder = "0";
   alignParticles = "0";
   alignDirection = "0 1 0";
   highResOnly = "1";
};


datablock ParticleData(HvySnowMistParticle) {
   dragCoefficient = "0";
   windCoefficient = "0.05";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "4000";
   lifetimeVarianceMS = "200";
   spinSpeed = "1";
   spinRandomMin = "-50";
   spinRandomMax = "50";
   useInvAlpha = "0";
   textureName = "rainmist";
   colors[0] = "0.3 0.3 0.3 0.0";
   colors[1] = "0.3 0.3 0.3 0.25";
   colors[2] = "0.3 0.3 0.3 0.25";
   colors[3] = "0.0787402 0.0787402 0.0787402 0.25";
   sizes[0] = "150";
   sizes[1] = "150";
   sizes[2] = "150";
   sizes[3] = "150";
   times[0] = "0.1";
   times[1] = "0.2";
   times[2] = "0.9";
   times[3] = "1";
};

datablock ParticleEmitterData(HvySnowMistEmitter) {
   ejectionPeriodMS = "12";
   periodVarianceMS = "0";
   ejectionVelocity = "140";
   velocityVariance = "50";
   ejectionOffset = "200";
   ejectionOffsetVariance = "100";
   thetaMin = "0";
   thetaMax = "15";
   phiReferenceVel = "0";
   phiVariance = "360";
   softnessDistance = "1";
   ambientFactor = "0";
   overrideAdvance = "1";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "HvySnowMistParticle";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";
   reverseOrder = "0";
   alignParticles = "0";
   alignDirection = "0 1 0";
   highResOnly = "1";
};

datablock PrecipitationData(SnowZ)
{
   type = 0;
   materialList = "snowflakes.dml";
   sizeX = 0.20;
   sizeY = 0.20;
   
   movingBoxPer = 0.35;
   divHeightVal = 1.5;
   sizeBigBox = 1;
   topBoxSpeed = 20;
   frontBoxSpeed = 30;
   topBoxDrawPer = 0.5;
   bottomDrawHeight = 40;
   skipIfPer = -0.3;
   bottomSpeedPer = 1.0;
   frontSpeedPer = 1.5;
   frontRadiusPer = 0.5;
};