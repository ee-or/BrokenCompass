
datablock ForceFieldBareData(windowForceField)
{
   fadeMS           = 1000;
   baseTranslucency = 0.20;
   powerOffTranslucency = 0.0;
   teamPermiable    = false;
   otherPermiable   = false;
   color            = "0.0 0.55 0.99";
   powerOffColor    = "0.0 0.0 0.0";
   targetNameTag    = 'Force Field';
   targetTypeTag    = 'ForceField'; 

   texture[0] = "skins/forcef1";
   texture[1] = "skins/forcef2";
   texture[2] = "skins/forcef3";
   texture[3] = "skins/forcef4";
   texture[4] = "skins/forcef5";

   framesPerSec = 1;
   numFrames = 1;
   scrollSpeed = 0;
   umapping = 20.0;
   vmapping = 20.0;
};

datablock ParticleData(waterFallParticle) {
   dragCoefficient = "0";
   windCoefficient = "0";
   gravityCoefficient = "2";
   inheritedVelFactor = "0";
   constantAcceleration = "1";
   lifetimeMS = "1400";
   lifetimeVarianceMS = "200";
   spinSpeed = "1";
   spinRandomMin = "-50";
   spinRandomMax = "50";
   useInvAlpha = "0";
   textureName          = "smoke02";
   colors[0]     = "0.7 0.8 1.0 0.1";
   colors[1]     = "0.7 0.8 1.0 0.1";
   colors[2]     = "0.7 0.8 1.0 0.1";
   colors[3]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 1;
   sizes[1]      = 6;
   sizes[2]      = 8.7;
   sizes[3]      = 8.7;
   times[0]      = 0.1;
   times[1]      = 0.25;
   times[2]      = 0.78;
   times[3]      = 1.0;
};

datablock ParticleEmitterData(waterFallEmitter) {
   ejectionPeriodMS = "10";
   periodVarianceMS = "0";
   ejectionVelocity = "10";
   velocityVariance = "0";
   ejectionOffset = "8";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "10";
   phiReferenceVel = "0";
   phiVariance = "360";
   overrideAdvance = "1";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "waterFallParticle";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";
   };


   datablock ParticleData(waterFallParticle2) {
   dragCoefficient = "1";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "1";
   lifetimeMS = "1000";
   lifetimeVarianceMS = "200";
   spinSpeed = "1";
   spinRandomMin = "-50";
   spinRandomMax = "50";
   useInvAlpha = "0";
   textureName          = "smoke02";
   colors[0]     = "0.7 0.8 1.0 0.2";
   colors[1]     = "0.7 0.8 1.0 0.2";
   colors[2]     = "0.7 0.8 1.0 0.2";
   colors[3]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 6;
   sizes[1]      = 6;
   sizes[2]      = 6;
   sizes[3]      = 6;
   times[0]      = 0.1;
   times[1]      = 0.25;
   times[2]      = 0.78;
   times[3]      = 1.0;
};

datablock ParticleEmitterData(waterFallEmitter2) {
   ejectionPeriodMS = "10";
   periodVarianceMS = "0";
   ejectionVelocity = "10";
   velocityVariance = "0";
   ejectionOffset = "6";
   ejectionOffsetVariance = "0";
   thetaMin = "90";
   thetaMax = "90";
   phiReferenceVel = "0";
   phiVariance = "360";
   overrideAdvance = "1";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "waterFallParticle2";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";
   };


datablock SeekerProjectileData(sharkProj)
{
   projectileShapeName = "shark.dts";
   scale = "8 8 8";
   emitterDelay        = -1;
   directDamage        = 1.0;
   hasDamageRadius     = false;
   indirectDamage      = 0.50;
   damageRadius        = 7.5;
   radiusDamageType    = $DamageType::impact;
   kickBackStrength    = 1;

   sound 				= "";
   explosion           = "";
   underwaterExplosion = "";
   splash              = DiscSplash;

   velInheritFactor  = 1;
   lifetimeMS        = 30000;  // exact ceiling for this class -- onAdd clamps to [500, 30000]

   muzzleVelocity    = 18;
   maxVelocity       = 18;     // == muzzleVelocity so it never accelerates past 18
   acceleration      = 0;      // belt-and-suspenders -- no ramp-up even before hitting maxVelocity

   turningSpeed          = 0;  // no target is ever assigned (see below), but zero it so it
                                // physically cannot steer even if that ever changes
   terrainAvoidanceSpeed = 0;  // disable the terrain-dodge steering entirely

   useFlechette = false;       // no submunition deployment

   explodeOnWaterImpact = false;

   hasLight    = false;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";

   // puffEmitter / exhaustEmitter / exhaustNodeName / casingShapeName / casingDeb left
   // unset -- no smoke trail or ejected casing, matches shark's original bare-hazard look
};
function sharkSpawner(){
   if(($MatchStarted + $missionRunning) == 2){
      if(!isObject(game.shark)){
         game.sharkDirection = (game.sharkDirection == 1) ? -1 : 1;
         %pos = game.sharkDirection == 1 ? (50 SPC getRandom(160, 300) SPC getRandom(220, 240)) : (550 SPC getRandom(160, 300) SPC getRandom(220, 240));
         %shark = new SeekerProjectile() {
            dataBlock        = sharkProj;
            initialDirection = game.sharkDirection SPC "0 0";
            initialPosition  = %pos;
            sourceSlot       = -1;
         };
         MissionCleanup.add(%shark);
         game.shark = %shark;
      }
      if(!isObject(game.shark2)){
         game.sharkDirection2 = (game.sharkDirection2 == -1) ? 1 : -1;
         %pos = game.sharkDirection2 == 1 ? (50 SPC getRandom(-160, -300) SPC getRandom(220, 240)) : (550 SPC getRandom(-160, -300) SPC getRandom(220, 240));
         %shark2 = new SeekerProjectile() {
            dataBlock        = sharkProj;
            initialDirection = game.sharkDirection2 SPC "0 0";
            initialPosition  = %pos;
            sourceSlot       = -1;
            sourceSlot       = 0;
            vehicleObject    = 0;
            targetTeam = 0;
            team = 1;
            targetObject = -1;
         };
         MissionCleanup.add(%shark2);
         game.shark2 = %shark2;
      }
   }
   if(isObject(importantStuff)){
      schedule(1000, 0, "sharkSpawner"); 
   }
}