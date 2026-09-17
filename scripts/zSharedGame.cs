//----------------------------------------------------------------------------
// Shared "boost cannon" visual/audio effect set.
//
// Used by: bombardment.mis (zBBGame.cs), anomaly.mis (zAnomalyGame.cs),
// firn.mis (zFirnGame.cs).
//
// Consolidated from three near-identical per-map copies (F-prefixed, A-prefixed,
// F-suffixed) into one shared set to save datablock IDs. Each map keeps its own
// trigger DATABLOCK and its own uniquely-named trigger-handling function
// (BoostTrigBB::, BoostTrigAnomaly::, BoostTrigFirn::) in its own Game.cs file,
// since Torque dispatches onEnterTrigger by datablock name - sharing one trigger
// datablock across all three maps would make them all fire the same (last-loaded)
// handler function instead of their own. Only the effect datablocks referenced
// BY NAME from within each map's own handler (particles, explosion, shockwave,
// launch projectile, sounds) are safe to share, and are the ones moved here.
//----------------------------------------------------------------------------

datablock AudioProfile(BoostSound)
{
   filename    = "fx/Bonuses/upward_straipass2_elevator.wav";
   description = AudioExplosion3d;
   preload = true;
};

datablock AudioProfile(BoostCannonExpSound)
{
   filename    = "fx/powered/turret_mortar_explode.wav";
   description = AudioExplosion3d;
   preload = true;
};

datablock ParticleData(BoostCannonSmokeParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.1;
   inheritedVelFactor   = 0.00;

   lifetimeMS           = 2000;
   lifetimeVarianceMS   = 150;

   textureName          = "bsmoke02";

   useInvAlpha = 1;
   spinRandomMin = -30.0;
   spinRandomMax = 30.0;

   colors[0]     = "0.2 0.2 0.2 1.0";
   colors[1]     = "0.2 0.2 0.2 1.0";
   colors[2]     = "0.2 0.2 0.2 0.0";

   sizes[0]      = 0.25;
   sizes[1]      = 4.5;
   sizes[2]      = 4.5;

   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(BoostCannonSmokeEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 1;

   ejectionVelocity = 14.25;
   velocityVariance = 0.50;

   thetaMin         = 0.0;
   thetaMax         = 90.0;
   lifetimeMS       = 1000;
   particles = "BoostCannonSmokeParticle";
};

datablock ParticleData(BoostCannonExplosionSmoke)
{
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = 1.0;
   inheritedVelFactor   = 0.025;
   lifetimeMS           = 100;
   lifetimeVarianceMS   = 0;
   textureName          = "particleTest";
   useInvAlpha =  0;
   spinRandomMin = -200.0;
   spinRandomMax =  200.0;

   colors[0]     = "0.9 0.3 0.0 1.0";
   colors[1]     = "0.9 0.3 0.0 1";
   colors[2]     = "0.9 0.3 0.1 1";
   sizes[0]      = 16.0;
   sizes[1]      = 16.0;
   sizes[2]      = 12.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(BoostHeavyExplosionSmokeEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;
   ejectionVelocity = 520.25;
   velocityVariance = 0.25;
   thetaMin         = 0.0;
   thetaMax         = 35.0;
   lifetimeMS       = 200;

   particles = "BoostCannonExplosionSmoke";
};

datablock ShockwaveData(BoostCannonShockwave)
{
   width = 30;
   numSegments = 32;
   numVertSegments = 7;
   velocity = 200;
   acceleration = 50.0;
   lifetimeMS = 600;
   height = 0.5;
   verticalCurve = 0.375;

   mapToTerrain = false;
   renderBottom = true;
   orientToNormal = true;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 3.0;

   times[0] = 1.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "0.5 0.5 0.0 1.0";
   colors[1] = "0.7 0.5 0.0 1.0";
   colors[2] = "0.9 0.3 0.0 1.0";
};

datablock ExplosionData(BoostCannonExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;

   delayMS = 200;

   offset = 5.0;

   playSpeed = 1.5;

   sizes[0] = "6.0 6.0 6.0";
   sizes[1] = "6.0 6.0 6.0";
   times[0] = 0.0;
   times[1] = 1.0;

   shockwave      = BoostCannonShockwave;
   emitter[0] = BoostCannonSmokeEmitter;
   emitter[1] = BoostHeavyExplosionSmokeEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 6.0 9.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 1;
   camShakeRadius = 150.0;
};

datablock LinearFlareProjectileData(BoostCannonEffect)
{
   projectileShapeName = "plasmabolt.dts";
   scale               = "0.1 0.1 0.1";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   indirectDamage      = 0.0;
   damageRadius        = 0.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Plasma;

   explosion           = "BoostCannonExplosion";

   dryVelocity       = 32;
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 0;
   lifetimeMS        = 125;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = -1;

   size[0]           = 0.2;
   size[1]           = 0.5;
   size[2]           = 0.1;

   numFlares         = 35;
   flareColor        = "1 0.75 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = PlasmaProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock StaticShapeData(mapScriptStart){
   catagory = "misc";
   shapeFile = "flag.dts";
};

function mapScriptStart::onAdd(%this, %obj){
   Parent::onAdd(%this, %obj);
   if(%obj.startScript !$= ""){
      call(%obj.startScript);
      error("mapScriptStart::onAdd() called startScript" SPC %obj.startScript);
   }
}
function mapScriptStart::onRemove(%this, %obj){
   Parent::onRemove(%this, %obj);
   if(%obj.endScript !$= ""){
      call(%obj.endScript);
      error("mapScriptStart::onRemove() called endScript" SPC %obj.endScript);
   }
}

datablock TriggerData(deathTrig){
   tickPeriodMS = 32;
};

function deathTrig::onEnterTrigger(%data, %trigger, %player){
   %player.scriptKill(%trigger.deathType);
}

function deathTrig::onleaveTrigger(%data, %trigger, %player){
   return;
}

function deathTrig::onTickTrigger(%data, %trig){
   return;
}

function physZoneDelete(){
   if(isObject(PZones)){
      PZones.delete();
   }
}