$bbGame::ionCannonMode = 1;// if one turns the game into lctf 0 just wipe the map and reset flags 

datablock ForceFieldBareData(CannonBlocker)
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

datablock ForceFieldBareData(powerLiftEffect)
{
   fadeMS           = 1000;
   baseTranslucency = 0.25;
   powerOffTranslucency = 0.0;
   teamPermiable    = true;
   otherPermiable   = true;
   // it's RGB (red green blue)
   color            = "0.0 1.0 0.0";
   powerOffColor    = "0.0 1.0 0.0";
   targetTypeTag    = 'ForceField';

   texture[0] = "skins/forcef1";
   texture[1] = "skins/forcef2";
   texture[2] = "skins/forcef3";
   texture[3] = "skins/forcef4";
   texture[4] = "skins/forcef5";

   framesPerSec = 10;
   numFrames = 5;
   scrollSpeed = -8;
   umapping = 1.0;
   vmapping = 0.15;
};

datablock ForceFieldBareData(ccScreenLines)
{
   fadeMS           = 1000;
   baseTranslucency = 0.25;
   powerOffTranslucency = 0.0;
   teamPermiable    = true;
   otherPermiable   = true;
   // it's RGB (red green blue)
   color            = "1.0 1.0 0.0";
   powerOffColor    = "1.0 1.0 0.0";
   targetTypeTag    = 'ForceField';

   texture[0] = "skins/forcef1";
   texture[1] = "skins/forcef2";
   texture[2] = "skins/forcef3";
   texture[3] = "skins/forcef4";
   texture[4] = "skins/forcef5";

   framesPerSec = 10;
   numFrames = 5;
   scrollSpeed = -8;
   umapping = 1.0;
   vmapping = 0.15;
};

datablock StaticShapeData(fireSwitchBB)
{
   catagory = "misc";
   shapefile = "switch.dts";
   isInvincible = true;
   alwaysAmbient = true;
   needsNoPower = true;
   emap = true;
};

datablock ItemData(lightCube){
   catagory = "misc";
   shapeFile = "tCube.dts";
   rotate = true;
   pickupRadius = 0;
   lightOnlyStatic = true;
   lightType = "PulsingLight";//ConstantLight
   lightColor = "0 1 1 1";
   lightTime = 1200;
   lightRadius = 0.5;
   computeCRC = false;
   emap = false;
};

datablock StaticShapeData(slideSwitch)
{
   catagory             = "misc";
   shapeFile            = "tCube.dts";
};
datablock StaticShapeData(targetCube)
{
   catagory             = "misc";
   shapeFile            = "targetCube.dts";
};
datablock StaticShapeData(transparentCube)
{
   catagory             = "misc";
   shapeFile            = "tCube.dts";
};
datablock StaticShapeData(cannonTip)
{
   catagory             = "misc";
   shapeFile            = "cannonTip.dts";
};

datablock StaticShapeData(bterObj)
{
   catagory             = "misc";
   shapeFile            = "bTer.dts";
};


datablock StaticShapeData(foeObj)
{
   catagory             = "misc";
   shapeFile            = "foeMark.dts";
};

datablock StaticShapeData(friendObj)
{
   catagory             = "misc";
   shapeFile            = "friendMark.dts";
};

datablock StaticShapeData(flagFoeObj)
{
   catagory             = "misc";
   shapeFile            = "flagIconFoe.dts";
};

datablock StaticShapeData(flagFriendObj)
{
   catagory             = "misc";
   shapeFile            = "flagIconFriend.dts";
};


datablock AudioDescription(AudioBIGXExplosion3d){
   volume   = 1.0;
   isLooping= false;

   is3D     = true;
   minDistance= 150.0;
   MaxDistance= 440.0;
   type     = $EffectAudioType;
   environmentLevel = 1.0;
};
datablock AudioDescription(AudioBIGBIGExplosion3d){
   volume   = 1.0;
   isLooping= false;
   is3D     = true;
   minDistance= 800.0;
   MaxDistance= 2000.0;
   type     = $EffectAudioType;
   environmentLevel = 1.0;
};

datablock AudioProfile(alarmSFX){
   filename    = "alarm.wav";
   description = "AudioBIGXExplosion3d";
   preload = true;
};

datablock AudioProfile(nukeHit){
   filename    = "nukeBoom.wav";
   description = "AudioBIGBIGExplosion3d";
   preload = true;
};
datablock AudioProfile(BMortarFireSound){
   filename    = "mortarBombFire.wav";
   description = "AudioBIGXExplosion3d";
   preload = true;
};


datablock ParticleData(FireSmoke){
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = 1.0;   
   inheritedVelFactor   = 0.025;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 0;
   textureName          = "smoke02";
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

datablock ParticleEmitterData(FireSmokeEmitter){
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;
   ejectionVelocity = 520.25;
   velocityVariance = 0.25;
   thetaMin         = 0.0;
   thetaMax         = 15.0;
   //lifetimeMS       = 60000;

   particles = "FireSmoke";
};

datablock ExplosionData(MCannonExplosion){
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;

   delayMS = 200;

   offset = 5.0;

   playSpeed = 1.5;

   sizes[0] = "6.0 6.0 6.0";
   sizes[1] = "6.0 6.0 6.0";
   times[0] = 0.0;
   times[1] = 1.0;

   //shockwave      = BoostCannonShockwave;
   emitter[0] = BoostCannonSmokeEmitter;
   emitter[1] = BoostHeavyExplosionSmokeEmitter;
  //emitter[2] = HeavyCrescentEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 6.0 9.0";
   camShakeAmp = "2.0 2.0 2.0";
   camShakeDuration = 1;
   camShakeRadius = 150.0;
};

datablock LinearFlareProjectileData(MCannonEffect){
   projectileShapeName = "plasmabolt.dts";
   scale               = "0.1 0.1 0.1";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   indirectDamage      = 0.0;
   damageRadius        = 0.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Plasma;

   explosion           = "MCannonExplosion";

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

   //activateDelayMS = 100;
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


datablock ExplosionData(CamExplosion){

   shakeCamera = true;
   camShakeFreq = "10.0 6.0 9.0";
   camShakeAmp = "8.0 8.0 8.0";
   camShakeDuration = 1;
   camShakeRadius = 32.0;
};

datablock ExplosionData(CamExplosion2){

   shakeCamera = true;
   camShakeFreq = "10.0 6.0 9.0";
   camShakeAmp = "2.0 2.0 2.0";
   camShakeDuration = 8;
   camShakeRadius = 32.0;
};
datablock LinearFlareProjectileData(CamShakeEffect){
   projectileShapeName = "plasmabolt.dts";
   scale               = "0.1 0.1 0.1";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   indirectDamage      = 0.0;
   damageRadius        = 0.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Plasma;

   explosion           = "CamExplosion";

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

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.01;
   size[1]           = 0.01;
   size[2]           = 0.01;


   numFlares         = 1;
   flareColor        = "1 0.75 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	//sound        = PlasmaProjectileSound;
   //fireSound    = PlasmaFireSound;
   //wetFireSound = PlasmaFireWetSound;
   
   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};
datablock LinearFlareProjectileData(CamShakeEffect2){
   projectileShapeName = "plasmabolt.dts";
   scale               = "0.1 0.1 0.1";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = false;
   indirectDamage      = 0.0;
   damageRadius        = 0.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Plasma;

   explosion           = "CamExplosion2";

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

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.01;
   size[1]           = 0.01;
   size[2]           = 0.01;


   numFlares         = 1;
   flareColor        = "1 0.75 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	//sound        = PlasmaProjectileSound;
   //fireSound    = PlasmaFireSound;
   //wetFireSound = PlasmaFireWetSound;
   
   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};
datablock ParticleData(thetaSExplosionParticle) {//fire
   dragCoefficient = "9";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "3000";
   lifetimeVarianceMS = "0";
   spinSpeed = "0";
   spinRandomMin = "-360";
   spinRandomMax = "720";
   useInvAlpha = "0";
   textureName = "smoke02";
   colors[0] = "0.992 0.4 0 1";
   colors[1] = "0.992 0.301961 0.00784314 1";
   colors[2] = "0.996078 0.301961 0.00784314 0";
   colors[3] = "0.980392 0.301961 0.0156863 0";
   sizes[0] = "100";
   sizes[1] = "100";
   sizes[2] = "100";
   sizes[3] = "100";
   times[0] = "0";
   times[1] = "0.1";
   times[2] = "0.2";
   times[3] = "0.3";
};

datablock ParticleEmitterData(thetaSExplosionEmitter) {//fire
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "655.35";
   velocityVariance = "0";
   ejectionOffset = "20";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "180";
   phiReferenceVel = "0";
   phiVariance = "360";
   ambientFactor = "0";
   overrideAdvance = "0";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "thetaSExplosionParticle";
   lifetimeMS = "500";
   lifetimeVarianceMS = "0";
   useInvAlpha = false;
   reverseOrder = "0";
   alignParticles = "0";
   alignDirection = "0 1 0";
};

datablock ParticleData(thetaSExplosionParticle2) {//smoke
   dragCoefficient = "7";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "15000";
   lifetimeVarianceMS = "0";
   spinSpeed = "0.083";
   spinRandomMin = "-10";
   spinRandomMax = "10";
   useInvAlpha = "1";
   animateTexture = "0";
   framesPerSec = "1";
   textureCoords[0] = "0 0";
   textureCoords[1] = "0 1";
   textureCoords[2] = "1 1";
   textureCoords[3] = "1 0";
   animTexTiling = "0 0";
   textureName = "bsmoke02";
   colors[0] = "0.529412 0.533333 0.533333 0.295";
   colors[1] = "0.537 0.537 0.541 0.238";
   colors[2] = "0.568627 0.568627 0.564706 0.292";
   colors[3] = "0.502 0.502 0.498 0";
   sizes[0] = "150";
   sizes[1] = "150";
   sizes[2] = "150";
   sizes[3] = "150";
   times[0] = "0";
   times[1] = "0.229167";
   times[2] = "0.6875";
   times[3] = "1";
};

datablock ParticleEmitterData(thetaSExplosionEmitter2) {//smoke
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "150";
   velocityVariance = "100.83";
   ejectionOffset = "0";
   ejectionOffsetVariance = "0";
   thetaMin = "30";
   thetaMax = "180";
   phiReferenceVel = "0";
   phiVariance = "360";
   softnessDistance = "0.0001";
   ambientFactor = "0";
   overrideAdvance = "0";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "thetaSExplosionParticle2";
   lifetimeMS = "1000";
   lifetimeVarianceMS = "0";
   
   
   blendStyle = "NORMAL";
   sortParticles = "1";
   reverseOrder = "0";
   alignParticles = "0";
   alignDirection = "0 1 0";
};

datablock ParticleData(thetaSExplosionParticleS) {
   dragCoefficient = "1";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "1";
   lifetimeMS = "9000";
   lifetimeVarianceMS = "0";
   spinSpeed = "0";
   spinRandomMin = "-360";
   spinRandomMax = "720";
   useInvAlpha = "0";
   textureName = "smoke02";
   colors[0] = "0.984 0.992 0.992 0.1";
   colors[1] = "0.984 0.984 0.992 0.1";
   colors[2] = "0.996 0.996 0.992 0.1";
   colors[3] = "0.996 0.996 0.992 0";
   sizes[0] = "150";
   sizes[1] = "150";
   sizes[2] = "150";
   sizes[3] = "150";
   times[0] = "0";
   times[1] = "0.0416667";
   times[2] = "0.125";
   times[3] = "0.375";
};

datablock ParticleEmitterData(thetaSExplosionEmitterS) { //wave
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "340.29";
   velocityVariance = "0";
   ejectionOffset = "100";
   ejectionOffsetVariance = "0";
   thetaMin = "";
   thetaMax = "180";
   phiReferenceVel = "0";
   phiVariance = "360";
   overrideAdvance = "0";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "thetaSExplosionParticleS";
   lifetimeMS = "250";
   lifetimeVarianceMS = "0";
   useInvAlpha = false;
   alignParticles = "0";
};

datablock ExplosionData(thetaSStrikeExplosion2){
   emitter[0] = thetaSExplosionEmitterS;
   emitter[1] = thetaSExplosionEmitterS;
   emitter[2] = thetaSExplosionEmitterS;
   emitter[3] = thetaSExplosionEmitterS;
   emitter[4] = thetaSExplosionEmitterS;
};

datablock ExplosionData(thetaSStrikeExplosion){
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = BombExplosionSound;
   faceViewer           = true;
   emitter[0] = thetaSExplosionEmitter;
   emitter[1] = thetaSExplosionEmitter2;
   subExplosion[0] = thetaSStrikeExplosion2;
   subExplosion[1] = thetaSStrikeExplosion2;
   subExplosion[2] = thetaSStrikeExplosion2;
   subExplosion[3] = thetaSStrikeExplosion2;
   subExplosion[4] = thetaSStrikeExplosion2;
   //emitter[2] = BlastRingEmitter;
   delayMS = 150;
   offset = 4.0;
   playSpeed = 0.8;

   sizes[0] = "70 70 70";
   sizes[1] = "70 70 70";
   times[0] = 0.0;
   times[1] = 1.0;
   shakeCamera = true;
   camShakeFreq = "8.0 9.0 7.0";
   camShakeAmp = "10.0 10.0 10.0";
   camShakeDuration = 2;
   camShakeRadius = 400.0;
};  
       
datablock LinearFlareProjectileData(ThetaCShot){
   projectileShapeName = "plasmabolt.dts";
   scale               = "6 6 6";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 100;
   damageRadius        = 400;
   kickBackStrength    = 10000.0;
   radiusDamageType    = $DamageType::Dark;
   Impulse = true;
   explosion           = "thetaSStrikeExplosion";
   underwaterExplosion = "UnderwaterMortarExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 900.0;
   wetVelocity       =  600;
   velInheritFactor  = 1;
   fizzleTimeMS      = 5000;
   lifetimeMS        = 5000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 20.0; 
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 6;
   size[1]           = 6;
   size[2]           = 6;


   numFlares         = 35;
   flareColor        = "1 0.75 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
   ignoreExEffects = 1;
};




datablock ParticleData(thetaSExplosionParticleMid) {//fire
   dragCoefficient = "9";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "3000";
   lifetimeVarianceMS = "0";
   spinSpeed = "0";
   spinRandomMin = "-360";
   spinRandomMax = "720";
   useInvAlpha = "0";
   textureName = "smoke02";
   colors[0] = "0.992 0.4 0 1";
   colors[1] = "0.992 0.301961 0.00784314 1";
   colors[2] = "0.996078 0.301961 0.00784314 0";
   colors[3] = "0.980392 0.301961 0.0156863 0";
   sizes[0] = "50";
   sizes[1] = "50";
   sizes[2] = "50";
   sizes[3] = "50";
   times[0] = "0";
   times[1] = "0.1";
   times[2] = "0.2";
   times[3] = "0.3";
};

datablock ParticleEmitterData(thetaSExplosionEmitterMid) {//fire
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "355.35";
   velocityVariance = "0";
   ejectionOffset = "20";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "180";
   phiReferenceVel = "0";
   phiVariance = "360";
   ambientFactor = "0";
   overrideAdvance = "0";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "thetaSExplosionParticleMid";
   lifetimeMS = "500";
   lifetimeVarianceMS = "0";
   useInvAlpha = false;
   reverseOrder = "0";
   alignParticles = "0";
   alignDirection = "0 1 0";
};

datablock ParticleData(thetaSExplosionParticle2Mid) {//smoke
   dragCoefficient = "7";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "15000";
   lifetimeVarianceMS = "0";
   spinSpeed = "0.083";
   spinRandomMin = "-10";
   spinRandomMax = "10";
   useInvAlpha = "1";
   animateTexture = "0";
   framesPerSec = "1";
   textureCoords[0] = "0 0";
   textureCoords[1] = "0 1";
   textureCoords[2] = "1 1";
   textureCoords[3] = "1 0";
   animTexTiling = "0 0";
   textureName = "bsmoke02";
   colors[0] = "0.529412 0.533333 0.533333 0.295";
   colors[1] = "0.537 0.537 0.541 0.238";
   colors[2] = "0.568627 0.568627 0.564706 0.292";
   colors[3] = "0.502 0.502 0.498 0";
   sizes[0] = "20";
   sizes[1] = "20";
   sizes[2] = "20";
   sizes[3] = "20";
   times[0] = "0";
   times[1] = "0.229167";
   times[2] = "0.6875";
   times[3] = "1";
};

datablock ParticleEmitterData(thetaSExplosionEmitter2MId) {//smoke
   ejectionPeriodMS = "4";
   periodVarianceMS = "0";
   ejectionVelocity = "20";
   velocityVariance = "5.83";
   ejectionOffset = "0";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "180";
   phiReferenceVel = "0";
   phiVariance = "360";
   softnessDistance = "0.0001";
   ambientFactor = "0";
   overrideAdvance = "0";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "thetaSExplosionParticle2Mid";
   lifetimeMS = "1000";
   lifetimeVarianceMS = "0";
   
   
   blendStyle = "NORMAL";
   sortParticles = "1";
   reverseOrder = "0";
   alignParticles = "0";
   alignDirection = "0 1 0";
};

datablock ExplosionData(thetaSStrikeMidExplosion){
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = BombExplosionSound;
   faceViewer           = true;
   emitter[0] = thetaSExplosionEmitterMid;
   emitter[1] = thetaSExplosionEmitter2MId;
   delayMS = 150;
   offset = 4.0;
   playSpeed = 0.8;

   sizes[0] = "30 30 30";
   sizes[1] = "30 30 30";
   times[0] = 0.0;
   times[1] = 1.0;
   shakeCamera = false;
};  
 
datablock LinearFlareProjectileData(midExpl){
   projectileShapeName = "plasmabolt.dts";
   scale               = "0.1 0.1 0.1";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 100.0;
   damageRadius        = 250.0;
   kickBackStrength    = 5000.0;
   radiusDamageType    = $DamageType::Explosion;

   explosion           = "thetaSStrikeMidExplosion";

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

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.01;
   size[1]           = 0.01;
   size[2]           = 0.01;


   numFlares         = 1;
   flareColor        = "1 0.75 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	//sound        = PlasmaProjectileSound;
   //fireSound    = PlasmaFireSound;
   //wetFireSound = PlasmaFireWetSound;
   
   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};



datablock ParticleData(thetaSExplosionParticleTop) {//fire
   dragCoefficient = "9";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "3000";
   lifetimeVarianceMS = "0";
   spinSpeed = "0";
   spinRandomMin = "-360";
   spinRandomMax = "720";
   useInvAlpha = "0";
   textureName = "smoke02";
   colors[0] = "0.992 0.4 0 1";
   colors[1] = "0.992 0.301961 0.00784314 1";
   colors[2] = "0.996078 0.301961 0.00784314 0";
   colors[3] = "0.980392 0.301961 0.0156863 0";
   sizes[0] = "100";
   sizes[1] = "100";
   sizes[2] = "100";
   sizes[3] = "100";
   times[0] = "0";
   times[1] = "0.1";
   times[2] = "0.2";
   times[3] = "0.3";
};

datablock ParticleEmitterData(thetaSExplosionEmitterTop) {//fire
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "655.35";
   velocityVariance = "0";
   ejectionOffset = "20";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "180";
   phiReferenceVel = "0";
   phiVariance = "360";
   ambientFactor = "0";
   overrideAdvance = "0";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "thetaSExplosionParticleTop";
   lifetimeMS = "500";
   lifetimeVarianceMS = "0";
   useInvAlpha = false;
   reverseOrder = "0";
   alignParticles = "0";
   alignDirection = "0 1 0";
};

datablock ParticleData(thetaSExplosionParticle2Top) {//smoke
   dragCoefficient = "7";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "15000";
   lifetimeVarianceMS = "0";
   spinSpeed = "0.083";
   spinRandomMin = "-10";
   spinRandomMax = "10";
   useInvAlpha = "1";
   animateTexture = "0";
   framesPerSec = "1";
   textureCoords[0] = "0 0";
   textureCoords[1] = "0 1";
   textureCoords[2] = "1 1";
   textureCoords[3] = "1 0";
   animTexTiling = "0 0";
   textureName = "bsmoke02";
   colors[0] = "0.529412 0.533333 0.533333 0.295";
   colors[1] = "0.537 0.537 0.541 0.238";
   colors[2] = "0.568627 0.568627 0.564706 0.292";
   colors[3] = "0.502 0.502 0.498 0";
   sizes[0] = "100";
   sizes[1] = "100";
   sizes[2] = "100";
   sizes[3] = "100";
   times[0] = "0";
   times[1] = "0.229167";
   times[2] = "0.6875";
   times[3] = "1";
};

datablock ParticleEmitterData(thetaSExplosionEmitter2Top) {//smoke
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "200";
   velocityVariance = "30.83";
   ejectionOffset = "0";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "90";
   phiReferenceVel = "0";
   phiVariance = "360";
   softnessDistance = "0.0001";
   ambientFactor = "0";
   overrideAdvance = "0";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "thetaSExplosionParticle2Top";
   lifetimeMS = "1000";
   lifetimeVarianceMS = "0";
   
   
   blendStyle = "NORMAL";
   sortParticles = "1";
   reverseOrder = "0";
   alignParticles = "0";
   alignDirection = "0 1 0";
};

datablock ExplosionData(thetaSStrikeTopExplosion){
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = BombExplosionSound;
   faceViewer           = true;
   emitter[0] = thetaSExplosionEmitterTop;
   emitter[1] = thetaSExplosionEmitter2Top;
   delayMS = 150;
   offset = 4.0;
   playSpeed = 0.8;

   sizes[0] = "50 50 50";
   sizes[1] = "50 50 50";
   times[0] = 0.0;
   times[1] = 1.0;
   shakeCamera = false;
}; 

 
 
 
datablock LinearFlareProjectileData(topExpl){
   projectileShapeName = "plasmabolt.dts";
   scale               = "0.1 0.1 0.1";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 100.0;
   damageRadius        = 250.0;
   kickBackStrength    = 5000.0;
   radiusDamageType    = $DamageType::Explosion;

   explosion           = "thetaSStrikeTopExplosion";

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

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 0.01;
   size[1]           = 0.01;
   size[2]           = 0.01;


   numFlares         = 1;
   flareColor        = "1 0.75 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

	//sound        = PlasmaProjectileSound;
   //fireSound    = PlasmaFireSound;
   //wetFireSound = PlasmaFireWetSound;
   
   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};


datablock ParticleData(NSmokeTrailParticle) {
   dragCoefficient = "1";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "100";
   lifetimeVarianceMS = "0";
   spinSpeed = "0";
   spinRandomMin = "-90";
   spinRandomMax = "500";
   useInvAlpha = "0";
   animateTexture = "0";
   framesPerSec = "1";
   textureCoords[0] = "0 0";
   textureCoords[1] = "0 1";
   textureCoords[2] = "1 1";
   textureCoords[3] = "1 0";
   animTexTiling = "0 0";
   textureName = "dParticle";
   colors[0] = "0.996078 0.301961 0.00784314 1";
   colors[1] = "0.996078 0.301961 0.00784314 0.77";
   colors[2] = "0.996078 0.301961 0.00784314 0.761";
   colors[3] = "0.996078 0.301961 0.00784314 1";
   sizes[0] = "13";
   sizes[1] = "13";
   sizes[2] = "13";
   sizes[3] = "13";
   times[0] = "0";
   times[1] = "0.270833";
   times[2] = "0.625";
   times[3] = "1";
};

datablock ParticleEmitterData(NSmokeTrailEmitter) {
   ejectionPeriodMS = "2";
   periodVarianceMS = "0";
   ejectionVelocity = "0";
   velocityVariance = "0";
   ejectionOffset = "0";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "15";
   phiReferenceVel = "0";
   phiVariance = "360";
   softnessDistance = "1";
   ambientFactor = "0";
   overrideAdvance = "1";
   orientParticles = "0";
   orientOnVelocity = "0";
   particles = "NSmokeTrailParticle";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";
   
   
   useInvAlpha = false;
   alignParticles = "0";
   alignDirection = "0 1 0";
   
};

datablock ParticleData(nukeFireParticle2) {
   dragCoefficient = "0.0";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "3000";//was 9000
   lifetimeVarianceMS = "1000";
   spinSpeed = "1";
   spinRandomMin = "-90";
   spinRandomMax = "500";
   useInvAlpha = "1";
   animateTexture = "0";
   framesPerSec = "1";
   textureCoords[0] = "0 0";
   textureCoords[1] = "0 1";
   textureCoords[2] = "1 1";
   textureCoords[3] = "1 0";
   animTexTiling = "0 0";
   textureName = "bsmoke02";
   colors[0] = "0.458824 0.458824 0.458824 1";
   colors[1] = "0.321569 0.321569 0.321569 0.496";
   colors[2] = "0.584314 0.584314 0.588235 1";
   colors[3] = "0.843137 0.847059 0.847059 0";
   sizes[0] = "15";
   sizes[1] = "15";
   sizes[2] = "15";
   sizes[3] = "15";
   times[0] = "0";
   times[1] = "0.329412";
   times[2] = "0.666667";
   times[3] = "1";
};

datablock ParticleEmitterData(nukeFireEmitter2) {
   ejectionPeriodMS = "15";
   periodVarianceMS = "0";
   ejectionVelocity = "0";
   velocityVariance = "0";
   ejectionOffset = "4.583";
   ejectionOffsetVariance = "0";
   thetaMin = "0";
   thetaMax = "5";
   phiReferenceVel = "0";
   phiVariance = "360";
   softnessDistance = "1";
   ambientFactor = "0";
   overrideAdvance = "1";
   orientParticles = "0";
   orientOnVelocity = "0";
   particles = "nukeFireParticle2";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";  
};
datablock SeekerProjectileData(nukeMissile)
{
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   scale = "10 10 10";
   hasDamageRadius     = true;
   indirectDamage      = 100;
   damageRadius        = 100.0;
   radiusDamageType    = $DamageType::Explosion;
   kickBackStrength    = 10000;

   explosion           = "thetaSStrikeExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage

   baseEmitter         = nukeFireEmitter2;
   delayEmitter        = NSmokeTrailEmitter;
   
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 17000; // z0dd - ZOD, 4/14/02. Was 6000
   muzzleVelocity      = 100.0;
   maxVelocity         = 300.0; // z0dd - ZOD, 4/14/02. Was 80.0
   turningSpeed        = 110.0;
   acceleration        = 100.0;

   proximityRadius     = 3;

   terrainAvoidanceSpeed         = 400;
   terrainScanAhead              = 8;
   terrainHeightFail             = 12;
   terrainAvoidanceRadius        = 100;  
   
   flareDistance = 0;
   flareAngle    = 0;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 15.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = true;
};

datablock ParticleData(ArtProjectileTrail01)
{
   textureName = "bsmoke02";
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = -0.15;   // rises slowly
   inheritedVelFactor   = 0.125;

   lifetimeMS           =  1500;
   lifetimeVarianceMS   =  600;
   useInvAlpha          =  true;
   spinRandomMin        = -100.0;
   spinRandomMax        =  100.0;


   colors[0]     = "0.4 0.4 0.4 0.5";
   colors[1]     = "0.3 0.3 0.3 0.8";
   colors[2]     = "0.0 0.0 0.0 0.0";
   sizes[0]      = 10.0;
   sizes[1]      = 20.0;
   sizes[2]      = 40.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
   dragCoefficient = "0";
};

datablock ParticleEmitterData(ArtProjectileTrailEmitter)
{
   ejectionPeriodMS = 12;
   periodVarianceMS = 0;

   ejectionVelocity = 2.25;
   velocityVariance = 0.55;

   reverseOrder = false;
   sortParticles    = true;
   overrideAdvance = false;
   thetaMin         = 0.0;
   thetaMax         = 40.0;
   particles = "ArtProjectileTrail01";
   useInvAlpha = "1";
};

datablock ParticleData(ArtSparks)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 150;
   textureName          = "special/bigSpark";
   colors[0]     = "0.56 0.36 0.26 1.0";
   colors[1]     = "0.56 0.36 0.26 1.0";
   colors[2]     = "1.0 0.36 0.26 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 0.75;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ArtSparksEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 40;
   velocityVariance = 20.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 180;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 200;
   particles = "ArtSparks";
};

datablock ParticleData(ArtExplosionSmoke)
{
   dragCoeffiecient     = 5;
   gravityCoefficient   = 8;   // rises slowly
   inheritedVelFactor   = 0;

   lifetimeMS           = 4000;
   lifetimeVarianceMS   = 0;

   textureName          = "smoke02";

   useInvAlpha =  true;
   spinRandomMin = -200.0;
   spinRandomMax =  200.0;

   textureName = "bsmoke02";

   colors[0]     = "0.6 0.6 0.6 1.0";
   colors[1]     = "0.4 0.4 0.4 0.5";
   colors[2]     = "0.0 0.0 0.0 0.0";
   sizes[0]      = 17.0;
   sizes[1]      = 17.0;
   sizes[2]      = 12.0;
   times[0]      = 0.0;
   times[1]      = 0.4;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ArtExplosionSmokeEmitter)
{
   ejectionPeriodMS = 2;
   periodVarianceMS = 0;

   ejectionVelocity = 150.25;
   velocityVariance = 2.25;

   thetaMin         = 0.0;
   thetaMax         = 180.0;

   lifetimeMS       = 200;

   particles = "ArtExplosionSmoke";
};


datablock ExplosionData(ArtSubExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   explosionScale = "0.5 0.5 0.5";


   lifetimeMS = 1000;
   delayMS = 0;

   emitter[0] = ArtExplosionSmokeEmitter;
   emitter[1] = ArtSparksEmitter;

   offset = 0.0;

   playSpeed = 1.5;

   sizes[0] = "21.5 21.5 21.5";
   sizes[1] = "30.0 30.0 30.0";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(ArtSubExplosion2)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   explosionScale = "8.7 8.7 8.7";

   lifetimeMS = 1000;
   delayMS = 50;

   emitter[0] = ArtExplosionSmokeEmitter;
   emitter[1] = ArtSparksEmitter;

   offset = 9.0;

   playSpeed = 1.5;

   sizes[0] = "1.5 1.5 1.5";
   sizes[1] = "1.5 1.5 1.5";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(ArtSubExplosion3)
{
   explosionShape = "effect_plasma_explosion.dts";
   faceViewer           = true;
   explosionScale = "10.0 10.0 10.0";

   debris = VehicleFireballDebris;
   debrisThetaMin = 10;
   debrisThetaMax = 170;
   debrisNum = 8;
   debrisVelocity = 60.0;
   debrisVelocityVariance = 15.0;

   lifetimeMS = 2000;
   delayMS = 100;

   emitter[0] = ArtExplosionSmokeEmitter;
   emitter[1] = ArtSparksEmitter;

   offset = 9.0;

   playSpeed = 2.5;

   sizes[0] = "1.0 1.0 1.0";
   sizes[1] = "1.0 1.0 1.0";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(ArtMainExplosion)
{
   soundProfile = BombExplosionSound;
   
   subExplosion[0] = ArtSubExplosion;
   subExplosion[1] = ArtSubExplosion2;
   subExplosion[2] = ArtSubExplosion3;
};

datablock GrenadeProjectileData(longGunProj)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 12.40;
   damageRadius        = 150.0;
   radiusDamageType    = $DamageType::Explosion;
   kickBackStrength    = 1500;
   bubbleEmitTime      = 1.0;

   //sound               = "";
   explosion           = ArtMainExplosion;
   underwaterExplosion = UnderwaterMortarExplosion;
   velInheritFactor    = 0.85; 

   baseEmitter         = ArtProjectileTrailEmitter;
   //delayEmitter        = longGunFireEmitter;
   bubbleEmitter       = MortarBubbleEmitter;

   grenadeElasticity = 0.30; 
   grenadeFriction   = 0.2;
   armingDelayMS     = 250;
   muzzleVelocity    = 400.00;
   gravityMod        = 0; 
};

datablock ParticleData(AtomicMortarProjectileTrail01)
{
   textureName = "bsmoke02";
   dragCoeffiecient     = 0.4;
   gravityCoefficient   = -0.15;   // rises slowly
   inheritedVelFactor   = 0.125;

   lifetimeMS           =  1500;
   lifetimeVarianceMS   =  600;
   useInvAlpha          =  true;
   spinRandomMin        = -100.0;
   spinRandomMax        =  100.0;


   colors[0]     = "0.5 0.8 0.5 0.5";
   colors[1]     = "0.3 0.7 0.3 0.8";
   colors[2]     = "0.0 0.0 0.0 0.0";
   sizes[0]      = 10.0;
   sizes[1]      = 20.0;
   sizes[2]      = 40.5;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
   dragCoefficient = "0";
};

datablock ParticleEmitterData(AtomicMortarProjectileTrailEmitter)
{
   ejectionPeriodMS = 12;
   periodVarianceMS = 0;

   ejectionVelocity = 2.25;
   velocityVariance = 0.55;

   reverseOrder = false;
   sortParticles    = true;
   overrideAdvance = false;
   thetaMin         = 0.0;
   thetaMax         = 40.0;
   particles = "AtomicMortarProjectileTrail01";
   blendStyle = "ADDITIVE";
   useInvAlpha = "1";
};

datablock ParticleData(AtomicMortarMainExplosionParticle)
{
   dragCoefficient      = 3.64;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0;
   constantAcceleration = 0.0;
   lifetimeMS           = 900;
   lifetimeVarianceMS   = 200;
   spinRandomMin = "-560";
   useInvAlpha          =  false;
   spinRandomMax =  "720";
   textureName          = "special/shockLightning02";
   colors[0]     = "0 1 0 1";
   colors[1]     = "0 1 0 0.35";
   colors[2]     = "0 0.1 0 0";
   sizes[0]      = 24.5;
   sizes[1]      = 26.8;
   sizes[2]      = 27.8;
   times[0]      = 0;
   times[1]      = 0.5;
   times[2]      = 1;
};

datablock ParticleEmitterData(AtomicMortarMainExplosionParticleEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 62.5;
   velocityVariance = 33.5;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   lifetimeMS       = 50;
   phiReferenceVel  = 0;
   blendStyle = "ADDITIVE";
   phiVariance      = 360;
   orientParticles  = false;
   reverseOrder = true;
   sortParticles    = true;
   overrideAdvance = true;
   softnessDistance = "0.0001";
   orientOnVelocity = 1;
   particles = "AtomicMortarMainExplosionParticle";
};

datablock ParticleData(AtomicMortarShockwaveParticle)
{
   dragCoefficient      = 0.2;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0;
   constantAcceleration = 0;
   lifetimeMS           = 3250;
   lifetimeVarianceMS   = 2500;
   spinRandomMin    = "-90";
   spinRandomMax    = "90";
   textureName          = "bsmoke02";
   colors[0]     = "0 1 0 0.6";
   colors[1]     = "0.5 0.9 0 0.3";
   colors[2]     = "0.5 0.5 0 0";
   sizes[0]      = 15.5;
   sizes[1]      = 27.8;
   sizes[2]      = 37.8;
   times[0]      = 0;
   times[1]      = 0.6;
   times[2]      = 1;
};

datablock ParticleEmitterData(AtomicMortarShockwaveParticleEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 4;
   velocityVariance = 3.9;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 180;
   lifetimeMS       = 50;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   orientParticles  = false;
   overrideAdvance = false;
   sortParticles    = true;
   softnessDistance = "0.0001";
   orientOnVelocity = 1;
   ambientFactor = "0.85";
   particles = "AtomicMortarShockwaveParticle";
};

datablock ExplosionData(AtomicMortarImpactExplosion)
{
   explosionShape = "mortar_explosion.dts";

   faceViewer           = true;

   delayMS = 100;

   offset = 5.0;

   playSpeed = 1.0;

   sizes[0] = "10.5 10.5 10.5";
   sizes[1] = "10.5 10.5 10.5";
   times[0] = 0.0;
   times[1] = 1.0;
   soundProfile   = MortarExplosionSound;
   emitter[0] = AtomicMortarMainExplosionParticleEmitter;
   emitter[1] = AtomicMortarShockwaveParticleEmitter;
   
   lightStartRadius = 24.0;
   lightEndRadius = 15.0;
   lightStartColor = "0.5 0.9 0 0.3";
   lightEndColor = "0.2 0.9 0 0.3";
   lightStartBrightness = 16.0;
   lightEndBrightness = 0.0;
   lightNormalOffset = 6.0;
   lifeTimeMS = "1484";
   
   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "30.0 30.0 30.0";
   camShakeDuration = 1.5;
   camShakeRadius = 20.0;
};


datablock GrenadeProjectileData(mortarGunProj)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 1.0;
   damageRadius        = 50.0;
   radiusDamageType    = $DamageType::Explosion;
   kickBackStrength    = 5000;
   bubbleEmitTime      = 1.0;

   //sound               = "";
   explosion           = AtomicMortarImpactExplosion;
   underwaterExplosion = UnderwaterMortarExplosion;
   velInheritFactor    = 0.85; 

   baseEmitter         = AtomicMortarProjectileTrailEmitter;
   delayEmitter        = "";
   emitterDelay = 32;
   bubbleEmitter       = MortarBubbleEmitter;

   grenadeElasticity = 0.30; 
   grenadeFriction   = 0.2;
   armingDelayMS     = 250;
   muzzleVelocity    = 150.00;
   gravityMod        = 3; 
};


function SceneObject::GetRealBoxSize(%obj){
   %box = %obj.getObjectBox();
   %x = getWord(%obj.getScale(),0);
   %y = getWord(%obj.getScale(),1);
   %z = getWord(%obj.getScale(),2);
   return mAbs(getWord(%box, 3) - getWord(%box, 0)) * %x SPC mAbs(getWord(%box, 4) - getWord(%box, 1)) * %y SPC  mAbs(getWord(%box, 5) - getWord(%box, 2)) * %z;
}
function firenukeMissile(%team, %hitPos){
   %otherTeam = (%team == 1) ? 2 : 1;
   %fobj = $TeamFlag[%otherTeam];
   %p = new LinearFlareProjectile() {
			dataBlock        = MCannonEffect;
			initialDirection = vectorScale( %team == 1 ? "-0.0553771 -0.73895 0.67148" : "-0.00302659 0.738099 0.674686",-1);
			initialPosition  = %team == 1 ? "-19.7233 780.329 264.729" : "-19.7233 -780.329 264.729";
			sourceObject     = -1;
			sourceSlot       = 0;
			vehicleObject    = 0;
   };
   MissionCleanup.add(%p);
   %c = new StaticShape() {
	 position = vectorAdd(%hitPos,"0 0 10");
	 rotation = "1 0 0 0";
	 scale = "0.1 0.1 0.1";
	 dataBlock = "targetCube";
  }; 
   MissionCleanup.add(%c);
   
   %p = new SeekerProjectile() {
	  dataBlock        = nukeMissile;
	  initialDirection = %team == 1 ? "-0.0553771 -0.73895 0.67148" : "-0.00302659 0.738099 0.674686";
	  initialPosition  = %team == 1 ? "-17.273 734.758 282d.281" : "-34.7393 -739.706 282.014";
	  sourceObject     = -1; 
	  sourceSlot       = 0;
	  vehicleObject    = 0;
	  targetTeam = %otherTeam;
	  team = %team;
	  targetObject = %c;
   };  
   
   %p.setScopeAlways();
   MissionCleanup.add(%p); 
   
   ServerPlay3D(alarmSFX, %otherTeam == 1 ? "-20.333 746.415 192.375" : "-36.703 -746.442 193.631");
	%p.setObjectTarget(%c);
}

function SpeedOfSound(%dist){
//speed of sound 340.29 m/s at sea level
   %delay = %dist / 340.29;
   %delay = %delay * 1000;
   return mFloor(%delay);
}
function nukeMissile::onExplode(%data, %proj, %pos, %mod) {
   
   for(%i = 0; %i < 8; %i++){
       %p = new LinearFlareProjectile() {
         dataBlock        = midExpl;
         initialDirection = "0 0 -1";
         initialPosition  = vectorAdd(%pos,"0 0" SPC 1+ %i * 20);
         sourceObject     = -1;
         sourceSlot       = 0;
         vehicleObject    = 0;
      };
      MissionCleanup.add(%p);   
   }
   %p = new LinearFlareProjectile() {
         dataBlock        = topExpl;
         initialDirection = "0 0 -1";
         initialPosition  = vectorAdd(%pos,"0 0" SPC 1 + %i * 20);
         sourceObject     = -1;
         sourceSlot       = 0;
         vehicleObject    = 0;
   };
   MissionCleanup.add(%p); 
   
   parent::onExplode(%data, %proj, %pos, %mod);
   %a = new AudioEmitter() {
			position = "0 0 200";
			rotation = "1 0 0 0";
			scale = "1 1 1";
			fileName = "nukeThud.wav";
			useProfileDescription = "0";
			outsideAmbient = "1";
			volume = "1";
			isLooping = "0";
			is3D = "0";
			minDistance = "20";
			maxDistance = "2048";
			coneInsideAngle = "360";
			coneOutsideAngle = "360";
			coneOutsideVolume = "1";
			coneVector = "0 0 1";
			loopCount = "-1";
			minLoopGap = "0";
			maxLoopGap = "0";
			type = "EffectAudioType";
   };
   %a.schedule(2000,"delete");
   MissionCleanup.add(%a);
   
   for(%idx = 0; %idx < ClientGroup.getCount(); %idx++){
      %client = ClientGroup.getObject(%idx);
      %obj  = %client.getControlObject();
      if(isObject(%obj)){
         %dist = vectorDist(%pos, %obj.getPosition());
         %client.schedule(SpeedOfSound(%dist),"play3D", nukeHit, %pos);
         schedule(SpeedOfSound(%dist), 0, "nukeCamShake",%obj);
      }
   }
}

function nukeCamShake(%obj){
   %p = new LinearFlareProjectile() {
      dataBlock        = CamShakeEffect;
      initialDirection = "0 0 -1";
      initialPosition  = %obj.getPosition();
      sourceObject     = -1;
      sourceSlot       = 0;
      vehicleObject    = 0;
   };
   MissionCleanup.add(%p);   
}

function nukeCamShake2(%obj){
   %p = new LinearFlareProjectile() {
      dataBlock        = CamShakeEffect2;
      initialDirection = "0 0 -1";
      initialPosition  = %obj.getPosition();
      sourceObject     = -1;
      sourceSlot       = 0;
      vehicleObject    = 0;
   };
   MissionCleanup.add(%p);   
}


function longGunProj::onExplode(%data, %proj, %pos, %mod){
   parent::onExplode(%data, %proj, %pos, %mod);
}

function fireLongGuns(%team){
   if(%team == 1){
      %v = 0;
      for(%x = 0; %x < 4; %x++){
         for(%i = 0; %i < team1LongGun.getCount(); %i++){
            %obj = team1LongGun.getObject(%i);
            schedule(%v++ * 512, 0, "fireBGun", %obj, %team); 
         }
      }
   }
   else if(%team == 2){
      %v = 0;
      for(%x = 0; %x < 4; %x++){
         for(%i = 0; %i < team2LongGun.getCount(); %i++){
            %obj = team2LongGun.getObject(%i);
            schedule(%v++ * 512, 0, "fireBGun", %obj, %team); 
         }   
      }
   }
   
}

function fireMortarGuns(%team){
   if(%team == 1){
      %v = 0;
      for(%x = 0; %x < 6; %x++){
         for(%i = 0; %i < team1MortarGuns.getCount(); %i++){
            %obj = team1MortarGuns.getObject(%i);
            schedule(%v++ * 256, 0, "fireMGun", %obj, %team); 
         }
      }
   }
   else if(%team == 2){
      %v = 0;
      for(%x = 0; %x < 6; %x++){
         for(%i = 0; %i < team2MortarGuns.getCount(); %i++){
            %obj = team2MortarGuns.getObject(%i);
            schedule(%v++ * 256, 0, "fireMGun", %obj, %team); 
         }
      }  
   }
   
}
function fireMGun(%obj, %team){
   %obj.playThread(1,"fire");  
   %obj.schedule(1500, "stopThread", 1); 
   %vec = %obj.getUpVector();
   %pos = vectorAdd(%obj.getPosition(), vectorScale(%vec,40));
   %p = new LinearFlareProjectile() {
            dataBlock        = MCannonEffect;
            initialDirection = vectorScale(%vec,-1);
            initialPosition  = %pos;
            sourceObject     = -1;
            sourceSlot       = 0;
            vehicleObject    = 0;
   };
   MissionCleanup.add(%p);
   
   %x = (getRandom() - 0.5) * 2 * 3.1415926 * 0.09;
   %y = (getRandom() - 0.5) * 2 * 3.1415926 * 0.09;
   %z = (getRandom() - 0.5) * 2 * 3.1415926 * 0.09;
   %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
   %vector = MatrixMulVector(%mat, %vec);
   %p = new GrenadeProjectile() {
            dataBlock        = mortarGunProj;
            initialDirection = %vector;
            initialPosition  = %pos;
            sourceObject     = -1;
            sourceSlot       = 0;
            vehicleObject    = 0;
   };
   MissionCleanup.add(%p);
   serverPlay3D(BMortarFireSound, %pos);
}

function fireBGun(%obj, %team){
   %obj.playThread(1,"fire");  
   %obj.schedule(1500, "stopThread", 1); 
   %vec = %obj.getUpVector();
   %pos = vectorAdd(%obj.getPosition(), vectorScale(%vec,70));
   %p = new LinearFlareProjectile() {
            dataBlock        = MCannonEffect;
            initialDirection = vectorScale(%vec,-1);
            initialPosition  = %pos;
            sourceObject     = -1;
            sourceSlot       = 0;
            vehicleObject    = 0;
   };
   MissionCleanup.add(%p);
   
   %p = new GrenadeProjectile() {
            dataBlock        = longGunProj;
            initialDirection = %vec;
            initialPosition  = %pos;
            sourceObject     = -1;
            sourceSlot       = 0;
            vehicleObject    = 0;
   };
   MissionCleanup.add(%p);
   serverPlay3D(BoostCannonExpSound, %pos);
   %p.schedule(6000, "delete");
   
   schedule(8000, 0, "dropLongProj", %team);
}

function dropLongProj(%team){
   %pos = Game.TargetX[%team] SPC Game.TargetY[%team] SPC 900;
   %rngPos = vectorAdd(%pos, getRandom(-100,100) SPC getRandom(-100,100) SPC 0);
   %p = new GrenadeProjectile() {
            dataBlock        = longGunProj;
            initialDirection = "0 0 -1";
            initialPosition  = %rngPos;
            sourceObject     = -1;
            sourceSlot       = 0;
            vehicleObject    = 0;
   };
   MissionCleanup.add(%p);
}

function bbSwitchInit(){
   for(%i = 0; %i < switchGrp.getCount(); %i++){
      %obj = switchGrp.getObject(%i);
      %obj.lastFired = getSimTime();
      %obj.digit[0] = "";
      %obj.digit[1] = "";
      %obj.digit[2] = "";
      %obj.digit[3] = "";
      %obj.digit[4] = "";
      if(%obj.displayPos !$= ""){
         updateTimers(%obj);
      }
   }
}


function bbGameStart(){
   if(isObject(PZones)){
      PZones.delete();
   }
   if(!isEventPending($BBSimEvent)){
      $tickCountBB = 0;
      new simGroup(ccSimObj);
      MissionCleanup.add(ccSimObj);    
      $BBSimEvent = schedule(10000, 0, "bbsim");
      schedule(1000, 0, "bbSwitchInit");
      $AmmoStation::mode = 0;
   }
} 

function bbGameEnd(){
   if (isActivePackage(bbSpawn)){
      deactivatePackage(bbSpawn);
      $AmmoStation::mode = 1;
   }
}

function SimObject::getUpVector(%obj){
   %rot = getWords(%obj.getTransform(), 3, 6);  
   %tmat = VectorOrthoBasis(%rot);
   return getWords(%tMat, 6, 8);
}
datablock TriggerData(BoostTrigBB){
   tickPeriodMS = 32;
};

function BoostTrigBB::onEnterTrigger(%data, %trigger, %player){

   if(%trigger.mode == 1){
      if(%trigger.ispowered()){
         %player.setPosition(%trigger.getWorldBoxCenter());
         %vel = VectorScale(VectorNormalize(%trigger.getUpVector()), 90);
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
   }
   else if(%trigger.mode == 2){
      if(%trigger.ispowered()){
         %trigPos = %trigger.getWorldBoxCenter();
         %player.setPosition(%trigPos);
         %vel = VectorScale(VectorNormalize(%trigger.getUpVector()), 150);
         %player.setVelocity(%vel);
         serverPlay3D(BoostCannonExpSound, %trigger.getTransform());
         %p = new LinearFlareProjectile() {
            dataBlock        = BoostCannonEffect;
            initialDirection = %trigger.getUpVector();
            initialPosition  = %trigPos;
            sourceObject     = %player;
            sourceSlot       = 0;
            vehicleObject    = 0;
         };
         MissionCleanup.add(%p);
      }
      else{
         messageClient(%player.client, 'MsgClient', '\c0Cannon is not powered.~wfx/powered/station_denied.wav');
      }
   }
}
function BoostTrigBB::onTickTrigger(%this, %triggerId){
 // anti spam
}

function pointToPosB(%posOne, %posTwo){
   %vec = vectorNormalize(VectorSub(%posTwo, %posOne));
   %x = getWord(%vec, 0);
   %y = getWord(%vec, 1);
   %z = getWord(%vec, 2);
    
   //---------X-----------------
   %rotAngleX = mASin(%z);
 
   //---------Z-----------------
   //get the angle for the z axis
   %rotAngleZ = mATan( %x, %y );
   //error(%rotAngleX SPC %rotAngleZ);
 
   //create 2 matrices, one for the z rotation, the other for the x rotation
   %matrix = MatrixCreateFromEuler("0 0" SPC %rotAngleZ * -1);
   %matrix2 = MatrixCreateFromEuler(%rotAngleX SPC "0 0");
 
   //now multiply them together so we end up with the rotation we want
   %finalMat = MatrixMultiply(%matrix, %matrix2);
 
   //we're done, send the proper numbers back
   return getWords(%finalMat, 3, 6);
} 

function drawTarget(%pos,%pos2, %tf){
   if(isObject(%tf)){
      %dist = vectorDist(%pos, %pos2);
      %rot = pointToPosB(%pos,%pos2);
      %mul = MatrixMulPoint(%pos SPC %rot,"-0.05 0 -0.05");
      %mID = new ForceFieldBare() {
         position = %mul;
         rotation = getWords(%rot,0,2) SPC mRadToDeg(getWord(%rot,3));
         scale = 0.1 SPC %dist SPC 0.1;
         dataBlock = "ccScreenLines";
         lockCount = "0";
         homingCount = "0";
         team = 0;
      };
      %tf.add(%mID);
   }
}

function updateTimers(%obj){
   if(digitGrp.getCount() > 40){
      digitGrp.delete();// something is not right prob form messing with things delete and reset
   }
   if(!isObject(digitGrp)){
      new simGroup(digitGrp);
      MissionCleanup.add(digitGrp);
   }
   if(%obj.lastFired $= ""){// bit of a fail safe 
      %obj.lastFired = getSimTime();
   }
   
   if(%obj.mode $= "Orbital"){
      %time = game.formatTime((60 * %obj.lockOutTime) - Game.turtleTime);
   }
   else{
      %time = game.formatTime((60 * %obj.lockOutTime) - (getSimTime() - %obj.lastFired));
   }

  
   if(%obj.mode !$= "Orbital" &&(60 * %obj.lockOutTime) - (getSimTime() - %obj.lastFired) < 0){
      %time =  "00:00";
   }
   else if(((60 * %obj.lockOutTime) - Game.turtleTime) < 0){

      %time =  "00:00";
   }

   %txtSPC = %obj.team == 1 ? -0.15 : 0.15;// spacing for the digits to be displayed on the switch
   %disOffset = %obj.team == 1 ? "0" : -0.35; // 0 is a string do to scripting bug other wise it always returns 0 no matter the team
   //error(%disOffset SPC %obj.team  SPC (%obj.team == 1));
   %rot = %obj.team == 1 ? "0 0 1 0" : "0 0 1 180";
   for(%i = 0; %i < 5; %i++){
      %posOffset = vectorAdd(%obj.displayPos, (%i * %txtSPC) + %disOffset SPC "0 0");
      %digit = getSubStr(%time, %i, 1);


      if(isObject(%obj.digit[%i]) && %obj.digit[%i].digit != %digit){
         %obj.digit[%i].delete();// time has changed lets update 
      }
      else{
         %obj.digit[%i].setScopeAlways();
      }

      if(%i == 2){
         if(!isObject(%obj.digit[%i])){
            %d = new TSStatic() {
               position = %posOffset;
               rotation = %rot;
               scale = "0.2 0.2 0.2";
               shapeName = "displayCol.dts";
               digit = %digit;
            };
            %obj.digit[%i] = %d;
            digitGrp.add(%d);
         }
      }
      else{
         if(!isObject(%obj.digit[%i])){
            %d = new TSStatic() {
               position = %posOffset;
               rotation = %rot;
               scale = "0.2 0.2 0.2";
               shapeName = "display" @ %digit @ ".dts";
               digit = %digit;
            };
            %obj.digit[%i] = %d;
            digitGrp.add(%d);
         }
      }
   }
}

function fireSwitchBB::onCollision(%data,%obj,%col)
{
   if (%col.getDataBlock().className $= Armor && %col.getState() !$= "Dead"){
      if(%col.team == %obj.team){
         switch$(%obj.mode){
            case "longGuns":
               if(isObject(Game.colObjX[%obj.team]) && isObject(Game.colObjY[%obj.team])){
                  if(!%obj.lastFired ||  (getSimTime() - %obj.lastFired) > 60 * %obj.lockOutTime){ 
                     fireLongGuns(%obj.team);
                     %obj.lastFired = getSimTime();
                     messageTeam(%obj.team, 'msgSwitchDenied', '\c2Firing Long Range Guns.~wfx/misc/red_alert_short.wav');    
                  }
                  else{
                     messageClient(%col.client, 'msgSwitchDenied', '\c2Reloading - Time Remaining %1.~wfx/powered/station_denied.wav', game.formatTime((60 * %obj.lockOutTime) - (getSimTime() - %obj.lastFired)));    
                  }
               }
               else{
                     messageClient(%col.client, 'msgSwitchDenied', '\c2No Valid Coordinates Selected.~wfx/powered/station_denied.wav');    
               }
            case "mortarGuns":
               if(!%obj.lastFired ||  (getSimTime() - %obj.lastFired) > 60 * %obj.lockOutTime){ 
                  fireMortarGuns(%obj.team);
                  %obj.lastFired = getSimTime();
                  messageTeam(%obj.team, 'msgSwitchDenied', '\c2Firing Short Range Mortars.~wfx/misc/red_alert_short.wav'); 
               }
               else{
                  messageClient(%col.client, 'msgSwitchDenied', '\c2Reloading - Time Remaining %1.~wfx/powered/station_denied.wav', game.formatTime((60 * %obj.lockOutTime) - (getSimTime() - %obj.lastFired)));    
               }
            case "nuke":
               if(isObject(Game.colObjX[%obj.team]) && isObject(Game.colObjY[%obj.team])){
                  %rayStart = Game.TargetX[%obj.team] SPC Game.TargetY[%obj.team] SPC 1000;
                  %rayEnd =Game.TargetX[%obj.team]SPC Game.TargetY[%obj.team] SPC - 1000;
                  %mask = $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType;
                  %ground = ContainerRayCast(%rayStart, %rayEnd, %mask, 0);
                  if(%ground){
                     if(!%obj.lastFired ||  (getSimTime() - %obj.lastFired) > 60 * %obj.lockOutTime){ 
                        firenukeMissile(%obj.team, getWords(%ground,1,3));
                        %obj.lastFired = getSimTime();
                        messageTeam(%obj.team, 'msgSwitchDenied', '\Launching Tac-Nuke.~wfx/misc/red_alert_short.wav');  
                     }
                     else{
                        messageClient(%col.client, 'msgSwitchDenied', '\c2Reloading - Time Remaining %1.~wfx/powered/station_denied.wav', game.formatTime((60 * %obj.lockOutTime) - (getSimTime() - %obj.lastFired)));    
                     }
                  }
                  else{
                     messageClient(%col.client, 'msgSwitchDenied', '\c2Could Not Find Target Try Again.~wfx/powered/station_denied.wav'); 
                  }
               }
               else{
                     messageClient(%col.client, 'msgSwitchDenied', '\c2No Valid Coordinates Selected.~wfx/powered/station_denied.wav');    
               }
            case "orbital":
               if(!%obj.lastFired ||  (Game.turtleTime > (%obj.lockOutTime*60))){ 
                  if(!Game.orbitalGunFire){
                     Game.orbitalGunFire = true;// lock it out as this is game over
                     fireOrbitGuns(%obj.team, %col.client);
                     messageAll('msgSwitchDenied', '\c2Firing Orbital Ion Cannon.~wfx/misc/red_alert_short.wav'); 
                  }
               }
               else{
                  %time = (60 * %obj.lockOutTime) - Game.turtleTime;
                  messageClient(%col.client, 'msgSwitchDenied', '\c2Only counts down during a stalemate/turtle - Time Remaining %1.~wfx/powered/station_denied.wav', game.formatTime(%time));
               }
         }
      }
      else{
         messageClient(%col.client, 'msgSwitchDenied', '\c2Access Denied -- Wrong team.~wfx/powered/station_denied.wav');    
      }
   }
}

function slideSwitch::onCollision(%data,%obj,%col){
   %yscale = getWord(%obj.getScale(),1);
   %scale = 1024/(%yscale/2);
   %objPos = %obj.getPosition();
   %colPos = %col.getPosition();
   if (%col.getDataBlock().className $= Armor && %col.getState() !$= "Dead"){
      if(%col.team == %obj.team){
         switch$(%obj.mode){
            case "setX":
               %x = getWord(vectorSub(%objPos,%colPos),0);
               Game.TargetX[%obj.team] = (%scale * %x) * -1;
               Game.colX[%obj.team] = getWord(%colPos,0);
               Game.colObjX[%obj.team] = %obj;
               drawXYTarget(%obj.team);
            case "setY":
               %y = getWord(vectorSub(%objPos,%colPos),1);
               Game.TargetY[%obj.team] = (%scale * %y) * -1; 
               Game.colY[%obj.team] = getWord(%colPos,1);
               Game.colObjY[%obj.team] = %obj;
               drawXYTarget(%obj.team);
         } 
         //error(Game.TargetX[%obj.team]  SPC Game.TargetY[%obj.team] );
      }
   }
}


function lightCube::onCollision(%data,%obj,%col){
   return;
}

function snapToGrid(%position, %gridSize) {
   %x = mFloor(getWord(%position, 0) / %gridSize + 0.5) * %gridSize;
   %y = mFloor(getWord(%position, 1) / %gridSize + 0.5) * %gridSize;
   %z = getWord(%position, 2); //mFloor(getWord(%position, 2) / %gridSize + 0.5) * %gridSize;
   
   return %x SPC %y SPC %z;
}

function drawXYTarget(%team){
   %tf  = targetFF @ %team;
   if(isObject(%tf)){
      %tf.delete();
   }
   new simGroup(%tf);
   
   MissionCleanup.add(%tf);
   %xObj = Game.colObjX[%team];
   if(isObject(%xObj)){
      %dir = (%team == 1) ? "0 25 0" : "0 -25 0";
      %x = Game.colX[%team];
      %pos = vectorAdd(setWord(%xObj.getPosition(),0,%x), %dir);
      drawTarget(setWord(%xObj.getPosition(),0,%x),%pos, %tf);
      if(!isObject(Game.tableLightX[%team])){
          %xLight = new Item() {
            position = "0 0 0";
            rotation = "1 0 0 0";
            scale = "0.5 0.5 0.5";
            dataBlock = "lightCube";
            collideable = "0";
            static = "1";
         };
		  MissionCleanup.add(%xLight);
         Game.tableLightX[%team] = %xLight;
      }
      %lpos =  vectorAdd(setWord(%xObj.getPosition(),0,%x),(%team == 1) ? "0 -0.2 0.5" :  "-0.2 0 0.5");
      %lpos = vectorAdd(snapToGrid(%lpos,0.5),(%team == 1) ? "0.22 0 0" :  "0 0.15 0");
      Game.tableLightX[%team].setTransform(%lpos SPC "1 0 0 0");
   }
   %yObj = Game.colObjY[%team];
   if(isObject(%yObj)){
      %dir = (%team == 1) ? "-25 0 0" : "25 0 0";
      %y = Game.colY[%team];
      %pos2 = vectorAdd(setWord(%yObj.getPosition(),1,%y), %dir);
      drawTarget(setWord(%yObj.getPosition(),1,%y),%pos2, %tf);
      if(!isObject(Game.tableLightY[%team])){
         %yLight = new Item() {
            position = "0 0 0";
            rotation = "1 0 0 0";
            scale = "0.5 0.5 0.5";
            dataBlock = "lightCube";
            collideable = "0";
            static = "1";
         };
		 MissionCleanup.add(%yLight);
         Game.tableLightY[%team] = %yLight;
      }
      %lpos = vectorAdd(setWord(%yObj.getPosition(),1,%y),(%team == 1) ? "0.2 0 0.5" : "-0.25 0 0.5");
      %lpos = vectorAdd(snapToGrid(%lpos,0.5), (%team == 1) ? "0 0.22 0" : "-0.15 0.0 0" );
      Game.tableLightY[%team].setTransform(%lpos SPC "1 0 0 0");
   }
   
   if(isObject(%xObj) && isObject(%yObj)){
      %cross = getWord(%pos,0) SPC getWords(%pos2,1, 2);   
      drawTarget(%cross,vectorAdd(%cross,"0 0 -4"), %tf);
   }
} 

function vcMul(%vec1,%vec2){
   return getWords(%vec1,0) * getWords(%vec2,0) SPC  getWords(%vec1,1) * getWords(%vec2,1) SPC getWords(%vec1,2) * getWords(%vec2,2);
}
function vcDiv(%vec1,%vec2){
   return getWords(%vec1,0) / getWords(%vec2,0) SPC  getWords(%vec1,1) / getWords(%vec2,1) SPC getWords(%vec1,2) / getWords(%vec2,2);
}
function bbsim(){
   if(($MatchStarted + $missionRunning) == 2 && ($HostGamePlayerCount - $HostGameBotCount > 0)){
      if($tickCountBB == 0){
         for(%t = 1; %t <= 2; %t++){
            %obj = $TeamFlag[%t];
            %obj.ccObj1 = "";
            %obj.ccObj2 = "";
         }
      }
      if(isObject(pZones)){// here for base reasions  
		   PZones.delete();
	   }
      %team1 = cctable1.getWorldBoxCenter();
      %scale1 = cctable1.GetRealBoxSize();
      %tableScale1 = vcDiv(%scale1, "2048 2048 270");
      
      %team2 = cctable2.getWorldBoxCenter();
      %scale2 = cctable2.GetRealBoxSize();
      %tableScale2 = vcDiv(%scale2, "2048 2048 270");
      
      for(%t = 1; %t <= 2; %t++){
         %obj = $TeamFlag[%t];
         if(isObject(%obj)){
            if(isObject(%obj.ccObj1)){
               %xform = isObject(%obj.carrier) == 1 ? %obj.carrier.getTransform() : %obj.getTransform();
               %plPos = getWords(%xform, 0, 2);
               %plRot = getWords(%xform, 3, 6);
               %tablePos = vectorAdd(%team1,vectorSub(vcMul(%plPos,%tableScale1),"0 0 1.8"));
               %obj.ccObj1.setTransform(%tablePos	SPC  %plRot);
            }
            else{
               if(%obj.team == 1){
                   %obj.ccObj1 = new StaticShape() {
                     position = "0 0 0";
                     rotation = "1 0 0 0";
                     scale = "0.25 0.25 0.25";
                     dataBlock = "flagFriendObj";
                  };  
                  ccSimObj.add(%obj.ccObj1); 
                  %obj.ccObj1.trackObj = %obj;  
               }
               else{
                  %obj.ccObj1 = new StaticShape() {
                     position = "0 0 0";
                     rotation = "1 0 0 0";
                     scale = "0.25 0.25 0.25";
                     dataBlock = "flagFoeObj";
                  };  
                  ccSimObj.add(%obj.ccObj1); 
                  %obj.ccObj1.trackObj = %obj;     
               }  
            }
            
            if(isObject(%obj.ccObj2)){
               %xform = isObject(%obj.carrier) == 1 ? %obj.carrier.getTransform() : %obj.getTransform();
               %plPos = getWords(%xform, 0, 2);
               %plRot = getWords(%xform, 3, 6);
               %tablePos = vectorAdd(%team2,vectorSub(vcMul(%plPos,%tableScale2),"0 0 1.8"));
               %obj.ccObj2.setTransform(%tablePos	SPC  %plRot);
            }
            else{
               if(%obj.team == 2){
                   %obj.ccObj2 = new StaticShape() {
                     position = "0 0 0";
                     rotation = "1 0 0 0";
                     scale = "0.25 0.25 0.25";
                     dataBlock = "flagFriendObj";
                  };  
                  ccSimObj.add(%obj.ccObj2); 
                  %obj.ccObj2.trackObj = %obj;  
               }
               else{
                  %obj.ccObj2 = new StaticShape() {
                     position = "0 0 0";
                     rotation = "1 0 0 0";
                     scale = "0.25 0.25 0.25";
                     dataBlock = "flagFoeObj";
                  };  
                  ccSimObj.add(%obj.ccObj2); 
                  %obj.ccObj2.trackObj = %obj;     
               }  
            }
         }
      }
         
      for(%i = 0; %i < ClientGroup.getCount(); %i++){
			%client = ClientGroup.getObject(%i);
			%player = %client.player;
			
			if(isObject(%player)){
			   if(isObject(%client.ccObj1)){
			      if(%client.team == 1 && %client.ccObj2.getDatablock() == foeObj.getID()){
                  %client.ccObj1.setDatablock(friendObj);
               }
               else if(%client.team == 2 && %client.ccObj2.getDatablock() == friendObj.getID()){
                  %client.ccObj1.setDatablock(foeObj);   
               }
               
               %xform = %player.getTransform();
               %plPos = getWords(%xform, 0, 2);
               %plRot = getWords(%xform, 3, 6);
               %tablePos = vectorAdd(%team1,vectorSub(vcMul(%plPos,%tableScale1),"0 0 1.8"));
               %client.ccObj1.setTransform(%tablePos	SPC  %plRot);		      
			      
			   }
			   else{
			      if(%client.team == 1){
			         %client.ccObj1 = new StaticShape() {
                     position = "0 0 0";
                     rotation = "1 0 0 0";
                     scale = "0.1 0.1 0.1";
                     dataBlock = "friendObj";
                  };  
                  ccSimObj.add(%client.ccObj1); 
                  %client.ccObj1.trackObj = %client; 
                  %client.ccObj1.lastSim = getSimTime(); 
			      }
			      else{
			        %client.ccObj1 = new StaticShape() {
                     position = "0 0 0";
                     rotation = "1 0 0 0";
                     scale = "0.1 0.1 0.1";
                     dataBlock = "foeObj";
                  };
                  ccSimObj.add(%client.ccObj1);
                  %client.ccObj1.trackObj = %client;
                  %client.ccObj1.lastSim = getSimTime(); 
			      }
			   }

            if(isObject(%client.ccObj2)){
               if(%client.team == 2 && %client.ccObj2.getDatablock() == foeObj.getID()){
                  %client.ccObj2.setDatablock(friendObj);
               }
               else if(%client.team == 1 && %client.ccObj2.getDatablock() == friendObj.getID()){
                  %client.ccObj2.setDatablock(foeObj);   
               }
               
               %xform = %player.getTransform();
               %plPos = getWords(%xform, 0, 2);
               %plRot = getWords(%xform, 3, 6);
               %tablePos = vectorAdd(%team2,vectorSub(vcMul(%plPos,%tableScale2),"0 0 1.8"));
               %client.ccObj2.setTransform(%tablePos	SPC  %plRot);		      
			      
			   }
			   else{
			      if(%client.team == 2){
			         %client.ccObj2 = new StaticShape() {
                     position = "0 0 0";
                     rotation = "1 0 0 0";
                     scale = "0.1 0.1 0.1";
                     dataBlock = "friendObj";
                  };  
                  ccSimObj.add(%client.ccObj2); 
                  %client.ccObj2.trackObj = %client; 
                  %client.ccObj2.lastSim = getSimTime(); 
			      }
			      else{
			        %client.ccObj2 = new StaticShape() {
                     position = "0 0 0";
                     rotation = "1 0 0 0";
                     scale = "0.1 0.1 0.1";
                     dataBlock = "foeObj";
                  };
                  ccSimObj.add(%client.ccObj2);
                  %client.ccObj2.trackObj = %client;
                  %client.ccObj2.lastSim = getSimTime(); 
			      }
			   }
			}
			else{
			   if(isObject(%client.ccObj1)){
			      %client.ccObj1.setTransform("0 0 -1000 0 0 1 0");   
			   }
			   if(isObject(%client.ccObj2)){
			      %client.ccObj2.setTransform("0 0 -1000 0 0 1 0");    
			   }
			}  
	   }
      $tickCountBB++;
      %trackObj = ccSimObj.getObject($tickCountBB % ccSimObj.getCount());
      if(!isObject(%trackObj.trackObj)){// if are tracked object is not valid any more delete icons 
         %trackObj.delete();   
      }

      if(Game.stalemate){
         Game.turtleTime += 512;
      }
      if(($tickCountBB % 2) == 0){
         for(%i = 0; %i < switchGrp.getCount(); %i++){
            %obj = switchGrp.getObject(%i);
            if(%obj.displayPos !$= ""){
               updateTimers(%obj);
            }
         }
      }
   }
	if(isObject(ccSimObj)){
		$BBSimEvent = schedule(512, 0, "bbsim");
	}
}



datablock ParticleData(OBLShockPart)
{
   textureName          = "firefly.png";
   dragCoefficient      = "0.371457";
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = "0.199609";
   windCoefficient      = 0;
   constantAcceleration = 0;
   lifetimeMS           = "15000";
   lifetimeVarianceMS   = "0";
   spinRandomMin = "0";
   spinRandomMax =  "1";
   useInvAlpha   = "0";

   colors[0]     = "0.463 0.463 0.463 0.057"; 
   colors[1]     = "0.404 0.404 0.404 0.03";
   colors[2]     = "0.514 0.51 0.51 0.051";
   colors[3] = "0.502 0.498 0.498 0";

   sizes[0]      = "1";
   sizes[1]      = "1";
   sizes[2]      = "1";
   sizes[3]      = "1";

   times[0]      = "0";
   times[1]      = "0.372549";
   times[2]      = "0.580392";
   times[3] = "1";

   spinSpeed = "0";
};

datablock ParticleEmitterData(OBLShockEmit)
{
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "2.5";
   velocityVariance = "0";
   ejectionOffset   = "0";
   thetaMin         = "90";
   thetaMax         = "90";
   phiReferenceVel  = 0;
   phiVariance      = 360;
   orientParticles  = "0";
   orientOnVelocity = true;
   overrideAdvances = false;
   particles = "OBLShockPart";
   lifetimeMS = "500";
};


datablock ParticleData(OBLShockPart2)
{
   textureName          = "firefly.png";
   dragCoefficient      = "0.371457";
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = "0.199609";
   windCoefficient      = 0;
   constantAcceleration = 0;
   lifetimeMS           = "15000";
   lifetimeVarianceMS   = "0";
   spinRandomMin = "0";
   spinRandomMax =  "1";
   useInvAlpha   = "0";

   colors[0]     = "0.463 0.463 0.463 0.01"; 
   colors[1]     = "0.404 0.404 0.404 0.08";
   colors[2]     = "0.514 0.51 0.51 0.08";
   colors[3] = "0.502 0.498 0.498 0";
   
   sizes[0]      = "1";
   sizes[1]      = "2";
   sizes[2]      = "3";
   sizes[3] = "3";

   times[0]      = "0";
   times[1]      = "0.1";
   times[2]      = "0.5";   
   times[3] = "1";

   spinSpeed = "0";
};

datablock ParticleEmitterData(OBLShockEmit2)
{
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "2.1";
   velocityVariance = "1.5";
   ejectionOffset   = "0";
   thetaMin         = "90";
   thetaMax         = "110";
   phiReferenceVel  = 0;
   phiVariance      = 360;
   orientParticles  = "0";
   orientOnVelocity = true;
   overrideAdvances = false;
   particles = "OBLShockPart2";
   lifetimeMS = "800";
};


datablock ParticleData(OBLFireBallPart)
{
   textureName          = "orbitGunPart.png";
   dragCoefficient      = "1";
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = "0.199609";
   windCoefficient      = 0;
   constantAcceleration = 0;
   lifetimeMS           = "15000";
   lifetimeVarianceMS   = "0";
   spinRandomMin = "0";
   spinRandomMax =  "1";
   useInvAlpha   = "0";

   colors[0]     = "0.897638 0.299213 0 0.787402"; 
   colors[1]     = "0.897638 0.188976 0 0.503937";
   colors[2]     = "0.897638 0.188976 0 0.275591";
   colors[3] = "0.897638 0.0944882 0 0.00787402";

   sizes[0]      = "0.9";
   sizes[1]      = "0.9";
   sizes[2]      = "0.9";
   sizes[3]      = "0.9";

   times[0]      = "0";
   times[1]      = "0.364706";
   times[2]      = "0.537255";   
   times[3] = "0.911765";

   spinSpeed = "0";
};
datablock ParticleEmitterData(OBLFireBallEmit)
{
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "1.5";
   velocityVariance = 0.5;
   ejectionOffset   = "0";
   thetaMin         = "0";
   thetaMax         = "176.3";
   phiReferenceVel  = 0;
   phiVariance      = 360;
   orientParticles  = "0";
   orientOnVelocity = true;
   overrideAdvances = false;
   particles = "OBLFireBallPart";
   lifetimeMS = "1000";

};

datablock ParticleData(OBLHotRingPart)
{
   textureName          = "orbitGunPart.png";
   dragCoefficient      = "0.564712";
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = "0";
   windCoefficient      = 0;
   constantAcceleration = 0;
   lifetimeMS           = "10000";
   lifetimeVarianceMS   = "0";
   spinRandomMin = "0";
   spinRandomMax =  "1";
   useInvAlpha   = "0";

   colors[0]     = "0.9 0.3 0.008 1"; 
   colors[1]     = "0.996 0.3 0.008 0.994";
   colors[2]     = "0.996 0.471 0.008 1";
   colors[3]     = "0.996078 0.533333 0.00784314 0";
   
   sizes[0]      = "0.7";
   sizes[1]      = "0.7";
   sizes[2]      = "0.7";
   sizes[3]      = "0.7";

   times[0]      = "0.145833";
   times[1]      = "0.279167";
   times[2]      = "0.466667";
   times[3]      = "1";

   spinSpeed = "0";
};

datablock ParticleEmitterData(OBLHotRingEmit)
{
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "2";
   velocityVariance = 0;
   ejectionOffset   = "0";
   thetaMin         = "90";
   thetaMax         = "90";
   phiReferenceVel  = 0;
   phiVariance      = 360;
    orientParticles  = "0";
   orientOnVelocity = true;
   overrideAdvances = false;
   particles = "OBLHotRingPart";
   lifetimeMS = "200";
};

datablock AudioProfile(ionAlarmSFX){
   filename    = "ionwarn.wav";
   description = "AudioBIGXExplosion3d";
   preload = true;
};



function fireOrbitGuns(%team, %client){
   %otherTeam = (%team == 1) ? 2 : 1;
   %targetPos = $TeamFlag[%otherTeam].originalPosition;
   //ionBeamStart(vectorAdd(%targetPos,"0 0 100"));
   ServerPlay3D(ionAlarmSFX, "-20.333 746.415 192.375");
   ServerPlay3D(ionAlarmSFX, "-36.703 -746.442 193.631");
   ServerPlay3D(ionAlarmSFX, "-20.333 746.415 192.375");
   ServerPlay3D(ionAlarmSFX, "-36.703 -746.442 193.631");
   ServerPlay3D(ionAlarmSFX, "-20.333 746.415 192.375");
   ServerPlay3D(ionAlarmSFX, "-36.703 -746.442 193.631");
   //messageAll('ServerAudio','~walarm.wav');
   commandToClient(%client, 'setHudMode', 'PickTeam');
   
   %client.schedule(7000, "setControlObject", %client.camera);
   schedule(8000, 0, "fireOrbitGunStart", %client);
}

function fireOrbitGunStart(%client){
   %client.camera.setTransform("4.60111 -1.10804 -436.073" SPC "0.324266 0.260938 -0.909265 83.0181");

   obGun.stopThread(0);
   obGun.schedule(2000, "playThread", 0, "open");
   schedule(2000, 0, "commandToClient", %client, 'ServerAudio', "~worbitGunPop.wav",0);
   schedule(2333, 0, "commandToClient", %client, 'ServerAudio', "~worbitGunCharge.wav",1);
   schedule(5333, 0, "fireOrbitGunCrossBeam", %client, 1);
   schedule(7333, 0, "fireOrbitGunCrossBeam", %client, 2);
   schedule(8333, 0, "commandToClient", %client, 'ServerAudio', "~worbitGunStage.wav",0);
   schedule(10500, 0, "fireOrbitGunBeam", %client);
}

function fireOrbitGunCrossBeam(%client, %beam){
   commandToClient(%client, 'ServerAudio', "~wbeamstrike.wav",2);
   if(%beam == 1){
      %obj = new StaticShape() {
         position = "3.95583 -0.886909 -436.632";
         rotation = "0 -1 0 56.1499";
         scale = "0.005 0.335549 0.004";
         nameTag = "Base";
         dataBlock = "orbitalBeam";
         lockCount = "0";
         homingCount = "0";

         Target = "-1";
      };
      MissionCleanup.add(%obj);
      %obj.schedule(15000,"delete");
   }
   else if(%beam == 2){
      %obj = new StaticShape() {
         position = "3.8527 -0.719244 -436.497";
         rotation = "0.894263 0.316334 0.316586 96.3445";
         scale = "0.005 0.335549 0.004";
         nameTag = "Base";
         dataBlock = "orbitalBeam";
         lockCount = "0";
         homingCount = "0";

         Target = "-1";
      };
      MissionCleanup.add(%obj);
      %obj.schedule(15000,"delete");
   }
}

function fireOrbitGunBeam(%client){
   %obj = new StaticShape() {
         position = "-8.58598 -0.602089 -446.815";
         rotation = "-0.314547 0.317455 0.894585 96.8931";
         scale = "0.01 16.4064 0.01";
         nameTag = "Base";
         dataBlock = "orbitalBeam";
         lockCount = "0";
         homingCount = "0";
         Target = "-1";
	};
   MissionCleanup.add(%obj);
   schedule(1, 0, "commandToClient", %client, 'ServerAudio', "~worbitGunFire.wav",1);
   schedule(1000, 0, "commandToClient", %client, 'ServerAudio', "~worbitGunImpact.wav",0);
   schedule(1000, 0, "fireOrbitGunImpact", %client);
   %obj.schedule(2000,"delete");
   schedule(15000, 0, "resetClientView", %client);
   schedule(13000, 0, "orbitGunBeamFireA", %client);
}

function fireOrbitGunImpact(%client){
   %client.getControlObject().setWhiteout(0.8); 
   %pos = "-7.60736 -0.555335 -446.065";
   %e = new ParticleEmissionDummy() { // outter shockwave
      position = %pos;
      rotation = "0 -1 0 50.9934";
      scale = "1 1 1";
      dataBlock = "doubleTimeEmissionDummy";
      lockCount = "0";
      homingCount = "0";
      emitter = "OBLShockEmit";
      velocity = "1";
   };
   MissionCleanup.add(%e);
   %e.schedule(9000,"delete");


   %e = new ParticleEmissionDummy() { //seconed shockwave
   
      position = %pos;
      rotation = "0 -1 0 50.9934";
      scale = "1 1 1";
      dataBlock = defaultEmissionDummy;
      lockCount = "0";
      homingCount = "0";
      emitter = "OBLShockEmit2";
      velocity = "1";
   };
   MissionCleanup.add(%e);
   %e.schedule(9000,"delete");


   %e = new ParticleEmissionDummy() { // fireball
      position = %pos;
      rotation = "1 0 0 0";
      scale = "1 1 1";
      dataBlock = "defaultEmissionDummy";
      lockCount = "0";
      homingCount = "0";
      emitter = "OBLFireBallEmit";
      velocity = "1";
   };
   MissionCleanup.add(%e);
   %e.schedule(9000,"delete");

   %e = new ParticleEmissionDummy() { // hot ring
      position = %pos;
      rotation = "0 -1 0 50.9934";
      scale = "1 1 1";
      dataBlock = "defaultEmissionDummy";
      lockCount = "0";
      homingCount = "0";
      emitter = "OBLHotRingEmit";
      velocity = "1";
   };
   MissionCleanup.add(%e);
   %e.schedule(9000,"delete");

   schedule(2000, 0, "fireObitGunCrack");
}

function fireObitGunCrack(){
   %obj = new TSStatic() {
		position = "-7.66393 -0.60618 -446.079";
		rotation = "0.41657 -0.0143753 0.90899 180.674";
		scale = "8 8 1";
		shapeName = "hotCracks.dts";
	};
   MissionCleanup.add(%obj);
   %obj.schedule(15000,"delete");
}


function resetClientView(%client){
   %camPoint = ObserverDropPoints.getObject(0);
   %client.camera.setTransform(%camPoint.getTransform());
   if(isObject(%client.player)){
      %client.setControlObject(%client.player);
      commandToClient(%client, 'setHudMode', 'Standard');
   }
   else{
      commandToClient(%client, 'setHudMode', 'Observer');
   }
}


datablock StaticShapeData(orbitalGun)
{
   catagory = "misc";
   shapefile = "orbitalGun.dts";
   emap = true;
};

datablock StaticShapeData(orbitalBeam)
{
   catagory = "misc";
   shapefile = "orbitalBeam.dts";
   emap = true;
   alwaysAmbient = true;
};

function aGameOver(){
   //$TeamScore[$darkWeaponTeam] += $Anomaly::darkWeaponBonus;
   //messageAll('MsgTeamScoreIs', "", $darkWeaponTeam, $TeamScore[$darkWeaponTeam]);
   messageAll('message', '\c2%1 used the orbital ion cannon to end game, %3 Bonus points for team %2.~wfx/misc/MA3.wav', $darkWeaponClient.name, $darkWeaponTeam, $Anomaly::darkWeaponBonus);
   
   Game.gameOver();
   cycleMissions();
}



datablock ParticleData(ionGunExpExplosionParticle) {
   dragCoefficient = "1.5";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "4000";
   lifetimeVarianceMS = "0";
   spinSpeed = "0.083";
   spinRandomMin = "-10";
   spinRandomMax = "10";
   useInvAlpha = "0";
   textureName = "bsmoke02";
   colors[0] = "0.992 0.4 0 1";
   colors[1] = "0.992 0.301961 0.00784314 1";
   colors[2] = "0.996078 0.301961 0.00784314 0";
   colors[3] = "0.980392 0.301961 0.0156863 0";
   sizes[0] = "150";
   sizes[1] = "150";
   sizes[2] = "150";
   sizes[3] = "150";
   times[0] = "0";
   times[1] = "0.229167";
   times[2] = "0.6875";
   times[3] = "1";
};

datablock ParticleEmitterData(ionGunExpExplosionEmitter) {//smoke
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "650";
   velocityVariance = "1";
   ejectionOffset = "0";
   thetaMin = "0";
   thetaMax = "90";
   phiReferenceVel = "0";
   phiVariance = "360";
   overrideAdvance = "0";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "ionGunExpExplosionParticle";
   lifetimeMS = "15000";
   lifetimeVarianceMS = "0";
};


datablock ParticleData(ionGunExpExplosionParticle2) {
   dragCoefficient = "2";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "15000";
   lifetimeVarianceMS = "0";
   spinSpeed = "0.083";
   spinRandomMin = "-10";
   spinRandomMax = "10";
   useInvAlpha = "1";
   textureName = "bsmoke02";
   colors[0] = "0.529412 0.533333 0.533333 0.495";
   colors[1] = "0.537 0.537 0.541 0.438";
   colors[2] = "0.568627 0.568627 0.564706 0.492";
   colors[3] = "0.502 0.502 0.498 0";
   sizes[0] = "150";
   sizes[1] = "150";
   sizes[2] = "150";
   sizes[3] = "150";
   times[0] = "0";
   times[1] = "0.229167";
   times[2] = "0.6875";
   times[3] = "1";
};

datablock ParticleEmitterData(ionGunExpExplosionEmitter2) {//smoke
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "650";
   velocityVariance = "600.83";
   ejectionOffset = "0";
   thetaMin = "0";
   thetaMax = "90";
   phiReferenceVel = "0";
   phiVariance = "360";
   overrideAdvance = "0";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "ionGunExpExplosionParticle2";
   lifetimeMS = "15000";
   lifetimeVarianceMS = "0";
};

datablock ParticleData(ionGunExpExplosionParticleS) {
   dragCoefficient = "1";
   windCoefficient = "0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "1";
   lifetimeMS = "9000";
   lifetimeVarianceMS = "0";
   spinSpeed = "0";
   spinRandomMin = "-360";
   spinRandomMax = "720";
   useInvAlpha = "0";
   textureName = "smoke02";
   colors[0] = "0.984 0.992 0.992 0.1";
   colors[1] = "0.984 0.984 0.992 0.1";
   colors[2] = "0.996 0.996 0.992 0.1";
   colors[3] = "0.996 0.996 0.992 0";
   sizes[0] = "150";
   sizes[1] = "150";
   sizes[2] = "150";
   sizes[3] = "150";
   times[0] = "0";
   times[1] = "0.0416667";
   times[2] = "0.125";
   times[3] = "0.375";
};

datablock ParticleEmitterData(ionGunExpExplosionEmitterS) { //wave
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   ejectionVelocity = "340.29";
   velocityVariance = "0";
   ejectionOffset = "100";
   ejectionOffsetVariance = "0";
   thetaMin = "";
   thetaMax = "180";
   phiReferenceVel = "0";
   phiVariance = "360";
   overrideAdvance = "0";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "ionGunExpExplosionParticleS";
   lifetimeMS = "512";
   lifetimeVarianceMS = "0";
   useInvAlpha = false;
   alignParticles = "0";
};

datablock ExplosionData(ionGunExpStrikeExplosion2){
   emitter[0] = ionGunExpExplosionEmitterS;
   emitter[1] = ionGunExpExplosionEmitterS;
   emitter[2] = ionGunExpExplosionEmitterS;
   emitter[3] = ionGunExpExplosionEmitterS;
   emitter[4] = ionGunExpExplosionEmitterS;
};

datablock ExplosionData(ionGunExpStrikeExplosion){
   explosionShape = "effect_plasma_explosion.dts";
   //soundProfile   = BombExplosionSound;
   faceViewer           = true;
   emitter[0] = ionGunExpExplosionEmitter;
   emitter[1] = ionGunExpExplosionEmitter2;
   subExplosion[0] = ionGunExpStrikeExplosion2;
   subExplosion[1] = ionGunExpStrikeExplosion2;
   subExplosion[2] = ionGunExpStrikeExplosion2;
   subExplosion[3] = ionGunExpStrikeExplosion2;
   subExplosion[4] = ionGunExpStrikeExplosion2;
   //emitter[2] = BlastRingEmitter;
   delayMS = 150;
   offset = 4.0;
   playSpeed = 0.8;

   sizes[0] = "70 70 70";
   sizes[1] = "70 70 70";
   times[0] = 0.0;
   times[1] = 1.0;
   shakeCamera = true;
   camShakeFreq = "8.0 9.0 7.0";
   camShakeAmp = "10.0 10.0 10.0";
   camShakeDuration = 2;
   camShakeRadius = 400.0;
};  

datablock LinearFlareProjectileData(ionCannonProj){
   projectileShapeName = "plasmabolt.dts";
   scale               = "0.1 0.1 0.1";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.0;
   damageRadius        = 0.0;
   kickBackStrength    = 5000.0;
   radiusDamageType    = $DamageType::Explosion;

   explosion           = "ionGunExpStrikeExplosion";

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

   size[0]           = 0.01;
   size[1]           = 0.01;
   size[2]           = 0.01;


   numFlares         = 1;
   flareColor        = "1 0.75 0.25";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   
   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "1 0.75 0.25";
};

datablock AudioProfile(ionCannonHit){
   filename    = "ionCannonHit.wav";
   description = "AudioBIGBIGExplosion3d";
   preload = true;
};

function orbitGunBeamFireA(%client){
   %otherTeam = (%client.team == 1) ? 2 : 1;
   %pos = $TeamFlag[%otherTeam].originalPosition;

   cancel($BBSimEvent);
   %a = new AudioEmitter() {
      position = "0 0 200";
      rotation = "1 0 0 0";
      scale = "1 1 1";
      fileName = "ionCannonBuildUp.wav";
      useProfileDescription = "0";
      outsideAmbient = "1";
      volume = "1";
      isLooping = "0";
      is3D = "0";
      minDistance = "20";
      maxDistance = "2048";
      coneInsideAngle = "360";
      coneOutsideAngle = "360";
      coneOutsideVolume = "1";
      coneVector = "0 0 1";
      loopCount = "-1";
      minLoopGap = "0";
      maxLoopGap = "0";
      type = "EffectAudioType";
   };
   %a.schedule(10000,"delete");
   MissionCleanup.add(%a);

   schedule(7000, 0, "orbitGunBeamFireB", %pos);

}

function orbitGunBeamFireB(%pos){
	%a = new StaticShape() {
		position = vectorAdd(%pos, "0 0 -20");
		rotation = "-1 0 0 90";
		scale = "10 2000 10";
		nameTag = "Base";
		dataBlock = "orbitalBeam";
		lockCount = "0";
		homingCount = "0";

	};
   %a.schedule(512,"delete");
   MissionCleanup.add(%a);

   schedule(128, 0, "orbitGunBeamFireC", %pos);

}


datablock StaticShapeData(ionFlareX)
{
   catagory             = "misc";
   shapeFile            = "ionFlare.dts";
};


function orbitGunBeamFireC(%pos){
   testSky(%pos);
   %p = new LinearFlareProjectile() {
      dataBlock        = ionCannonProj;
      initialDirection = "0 0 -1";
      initialPosition  = vectorAdd(%pos,"0 0" SPC 1+ %i * 20);
      sourceObject     = -1;
      sourceSlot       = 0;
      vehicleObject    = 0;
   };
   MissionCleanup.add(%p);   

   %a = new AudioEmitter() {
			position = "0 0 200";
			rotation = "1 0 0 0";
			scale = "1 1 1";
			fileName = "ionCannonThud.wav";
			useProfileDescription = "0";
			outsideAmbient = "1";
			volume = "1";
			isLooping = "0";
			is3D = "0";
			minDistance = "20";
			maxDistance = "2048";
			coneInsideAngle = "360";
			coneOutsideAngle = "360";
			coneOutsideVolume = "1";
			coneVector = "0 0 1";
			loopCount = "-1";
			minLoopGap = "0";
			maxLoopGap = "0";
			type = "EffectAudioType";
   };
   %a.schedule(15000,"delete");
   MissionCleanup.add(%a);
   
   for(%idx = 0; %idx < ClientGroup.getCount(); %idx++){
      %client = ClientGroup.getObject(%idx);
      %obj  = %client.getControlObject();
      if(isObject(%obj) && %obj.getClassName() $= "Camera"){
         %obj.setWhiteout(0.85); 
         %dist = vectorDist(%pos, %obj.getPosition());
         %client.schedule(SpeedOfSound(%dist),"play2D", ionCannonHit);
         %obj.schedule(SpeedOfSound(%dist), %pos, 50, $DamageType::Explosion);
      }
   }

   %damageMasks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType |
                  $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType |
                  $TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType;
   InitContainerRadiusSearch(%pos,  2048, %damageMasks); 
   while ((%targetObject = containerSearchNext()) != 0){
      %dist = vectorDist(%pos, %targetObject.getPosition());
      if((%targetObject.getType() & $TypeMasks::PlayerObjectType)){
         %targetObject.setWhiteout(0.85); 
         %targetObject.client.schedule(SpeedOfSound(%dist),"play2D", ionCannonHit);
         schedule(SpeedOfSound(%dist), 0, "nukeCamShake2",%obj);
      }
      %targetObject.schedule(SpeedOfSound(%dist), "damage", %pos, 50, $DamageType::Explosion);
    }
    if($bbGame::ionCannonMode){
      schedule(5000, 0, "spawnLCTFItems");
    }
    else{
      schedule(5000, 0, "terSwapTest");
    }
   schedule(8000, 0, "bbUpdatePlayers");
}


function testSky(%pos){

   Sky.delete();
   new Sky(Sky) {
		position = "-1216 -848 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "1600";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.390000 0.390000 0.390000 0.000000";
		fogDistance = "1500";
		fogColor = "0.500000 0.500000 0.500000 1.000000";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "Lush_l4.dml";
		windVelocity = "1 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 0";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0";
		high_visibleDistance = "-1";
		high_fogDistance = "-1";
		high_fogVolume1 = "-1 1.78457e+25 1.76114e+19";
		high_fogVolume2 = "-1 3.1807e-27 3.1928e-27";
		high_fogVolume3 = "-1 0 3.19284e-27";

		locked = "true";
		cloudSpeed0 = "0.000000 0.000000";
	};

   MissionCleanup.add(Sky);
   %a = new StaticShape() {
		position = %pos;
		rotation = "1 0 0 0";
		scale = "10 10 10";
		nameTag = "Base";
		dataBlock = "ionFlareX";
		lockCount = "0";
		homingCount = "0";
		Target = "-1";
	};
   MissionCleanup.add(%a);
   %a.schedule(19000,"delete");
   expandAtSpeed(%a, 1500, 343);

}


function spawnLCTFItems(){

   if(!isActivePackage(bbSpawn)){
      activatePackage(bbSpawn);
      Game.spawnBypass = true;
   }

   new TerrainBlock(Terrain2) {
		rotation = "1 0 0 0";
		scale = "1 1 1";
		detailTexture = "details/snowdet2";
		terrainFile = "bbAfterMath.ter";
		squareSize = "8";
		emptySquares = "";
      position = "-1024 -1024 0";
	};  

   Sky.delete();
   new Sky(Sky) {
		position = "-1216 -848 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "400";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.390000 0.390000 0.390000 0.000000";
		fogDistance = "300";
		fogColor = "0.500000 0.500000 0.500000 1.000000";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "Lush_l4.dml";
		windVelocity = "1 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 0";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0";
		high_visibleDistance = "-1";
		high_fogDistance = "-1";
		high_fogVolume1 = "-1 1.78457e+25 1.76114e+19";
		high_fogVolume2 = "-1 3.1807e-27 3.1928e-27";
		high_fogVolume3 = "-1 0 3.19284e-27";

		locked = "true";
		cloudSpeed0 = "0.000000 0.000000";
	};
   if(!isActivePackage(bbSpawn)){
      activatePackage(bbSpawn);
   }
   $TeamFlag[1].setTransform("-231.889 90.3593 642.733 0 0 1 120");
   $TeamFlag[1].originalPosition = "-231.889 90.3593 642.733";

   $TeamFlag[2].setTransform("233.986 -91.1905 642.713 0 0 -1 60");
   $TeamFlag[2].originalPosition = "233.986 -91.1905 642.713";


   %a = new TSStatic() {
		position = "-239.643 70.4626 636.839";
		rotation = "-0.0913773 -0.486135 0.869093 198.556";
		scale = "1 1 1";
		shapeName = "bbStandBrokenTop.dts";
	};
   MissionCleanup.add(%a);
   %a = new TSStatic() {
		position = "-231.889 90.3593 632.253";
		rotation = "0 0 1 201.291";
		scale = "1 1 1";
		shapeName = "bbStandBrokenBtm.dts";
	};
   MissionCleanup.add(%a);


   %a = new TSStatic() {
		position = "238.497 -73.4026 636.975";
		rotation = "0.916437 -0.17226 0.361205 54.9839";
		scale = "1 1 1";
		shapeName = "bbStandBrokenTop.dts";
	};
   MissionCleanup.add(%a);
   %a = new TSStatic() {
		position = "233.986 -91.1905 632.333";
		rotation = "0 0 1 21.291";
		scale = "1 1 1";
		shapeName = "bbStandBrokenBtm.dts";
	};
   MissionCleanup.add(%a);


   %a = new Item() {
		position = "-260.618 60.9086 636.086";
		rotation = "0 0 1 107.326";
		scale = "1 1 1";
		dataBlock = "T2AmmoDeployable";
		lockCount = "0";
		homingCount = "0";
		collideable = "0";
		static = "1";
		rotate = "0";

		Target = "-1";
	};
   MissionCleanup.add(%a);
	%a = new Item() {
		position = "-261.273 58.8084 636.086";
		rotation = "0 0 1 107.326";
		scale = "1 1 1";
		dataBlock = "T2AmmoDeployable";
		lockCount = "0";
		homingCount = "0";
		collideable = "0";
		static = "1";
		rotate = "0";

		Target = "-1";
	};
   MissionCleanup.add(%a);
	%a = new Item() {
		position = "259.082 -64.4557 635.665";
		rotation = "0 0 -1 71.0468";
		scale = "1 1 1";
		dataBlock = "T2AmmoDeployable";
		lockCount = "0";
		homingCount = "0";
		collideable = "0";
		static = "1";
		rotate = "0";

		Target = "-1";
	};
   MissionCleanup.add(%a);
	%a = new Item() {
		position = "258.413 -66.404 635.665";
		rotation = "0 0 -1 71.0468";
		scale = "1 1 1";
		dataBlock = "T2AmmoDeployable";
		lockCount = "0";
		homingCount = "0";
		collideable = "0";
		static = "1";
		rotate = "0";

		Target = "-1";
	};
   MissionCleanup.add(%a);

   %a = new ParticleEmissionDummy() {
		position = "-239.643 70.4626 600";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "defaultEmissionDummy";
		lockCount = "0";
		homingCount = "0";
		emitter = "midMapSmokeEmitter";
		velocity = "1";

		locked = "true";
	};
   MissionCleanup.add(%a);

   %a = new ParticleEmissionDummy() {
		position = "238.497 -73.4026 600";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		dataBlock = "defaultEmissionDummy";
		lockCount = "0";
		homingCount = "0";
		emitter = "midMapSmokeEmitter";
		velocity = "1";

		locked = "true";
	};
   MissionCleanup.add(%a);
}

function terSwapTest(){
 	new TerrainBlock(Terrain2) {
		rotation = "1 0 0 0";
		scale = "1 1 1";
		detailTexture = "details/snowdet2";
		terrainFile = "bombardmentFlat.ter";
		squareSize = "8";
		emptySquares = "";
      position = "-1024 -1024 0";
	};  
}


function expandAtSpeed(%obj, %finalScale, %speed)
{
    %dt = 33; // ~30 FPS
    expandTick(%obj, %finalScale, %speed, %dt);
}

function expandTick(%obj, %finalScale, %speed, %dt)
{
    if (!isObject(%obj))
        return;

    // Current uniform scale (uses X only)
    %current = getWord(%obj.getScale(), 0);

    // Stop if at or above final scale
    if (%current >= %finalScale)
    {
        %obj.setScale(%finalScale SPC %finalScale SPC %finalScale);
        return;
    }

    // Increase per tick
    %step = %speed * (%dt / 1000);

    %new = %current + %step;
    if (%new > %finalScale)
        %new = %finalScale;

    %obj.setScale(%new SPC %new SPC %new);

    // Continue
    schedule(%dt, 0, expandTick, %obj, %finalScale, %speed, %dt);
}

function bbUpdatePlayers(){
   Game.spawnBypass = 1;
   for(%i = 0; %i < ClientGroup.getCount(); %i++){
      %client = ClientGroup.getObject(%i);
      if(isObject(%client.player)){
         %newPos = Game.pickPlayerSpawn(%client, 0);
         %client.player.setPosition(%newPos);
      }
   }
}

package bbSpawn{
   function DefaultGame::pickPlayerSpawn(%game, %client, %respawn){
      if(Game.spawnBypass){
         %team = %client.team;
         %pos = $TeamFlag[%team].getPosition();
         for(%i = 0; %i < 25; %i++){
            %randx = getWord(%pos,0)+getRandom(-150,150);
            %randy = getWord(%pos,1)+getRandom(-150,150);
            %mask = $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType;
            %rayStart = %randx SPC %randy SPC "1000";
            %rayEnd = %randx SPC %randy SPC "-1000";
            %ground = ContainerRayCast(%rayStart, %rayEnd, %mask, 0);
            if(%ground)
            {
               %loc = getWord(%ground,1) SPC getWord(%ground,2) SPC getWord(%ground,3)+0.1;
               break;
            }
         }
         if(%ground)
            return %loc;
         return parent::pickPlayerSpawn(%game, %client, %respawn);

      }
      else{
         return parent::pickPlayerSpawn(%game, %client, %respawn);
      }
   }
};

datablock ParticleData(standSmokeParticle) {
   dragCoefficient = "0";
   windCoefficient = "0";
   gravityCoefficient = "-0.5";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "6000";
   lifetimeVarianceMS = "500";
   spinSpeed = "1";
   spinRandomMin = "-50";
   spinRandomMax = "50";
   useInvAlpha = "1";
   textureName = "hotSmoke";
   colors[0] = "0.604724 0.604724 0.604724 0.2";
   colors[1] = "0.6291339 0.691339 0.691339 0.2";
   colors[2] = "0.659843 0.659843 0.659843 0.2";
   colors[3] = "0.0787402 0.0787402 0.0787402 0.0";
   sizes[0] = "50";
   sizes[1] = "50";
   sizes[2] = "50";
   sizes[3] = "50";
   times[0] = "0.1";
   times[1] = "0.4";
   times[2] = "0.8";
   times[3] = "1";
};

datablock ParticleEmitterData(standSmokeEmitter) {
   ejectionPeriodMS = "10";
   periodVarianceMS = "0";
   ejectionVelocity = "5";
   velocityVariance = "3";
   ejectionOffset = "100";
   ejectionOffsetVariance = "50";
   thetaMin = "20";
   thetaMax = "90";
   phiReferenceVel = "0";
   phiVariance = "360";
   overrideAdvance = "1";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "standSmokeParticle";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";
   reverseOrder = "0";
   alignParticles = "0";
   alignDirection = "0 1 0";
   highResOnly = "1";
};
