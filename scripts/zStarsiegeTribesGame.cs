autoExec("scripts/StarsiegeTribes.cs",0,0);
$T1ScriptExec = 1;

$T1OnlyWeapons = 0;
$T1RetroArmor = 0;
$T1RetroSkin = 0;
$T1RetroFlag = 0;


datablock StaticShapeData(T1StartObj)
{
   catagory             = "misc";
   shapeFile            = "t1baseflag.dts";
};
function T1StartObj::onAdd(%this, %obj){  
   Parent::onAdd(%this, %obj);
   if(!isObject(StarsiegeTribesMap)){
      %obj.setName("StarsiegeTribesMap");
      StarsiegeTribesMap.teamOnly = 0;
      StarsiegeTribesMap.pureT1 = 0;
   }

   $T1OnlyWeapons = StarsiegeTribesMap.pureT1 == 1;
   $T1RetroArmor = StarsiegeTribesMap.pureT1 == 1;
   $T1RetroSkin = StarsiegeTribesMap.pureT1 == 1;
   $T1RetroFlag = StarsiegeTribesMap.pureT1 == 1;

   for(%i = 0; %i <= 4; %i++){
      $TeamDeployedCount[%i, T1RemoteTurret] = 0;
      $TeamDeployedCount[%i, T1InvyDeployable] = 0;
      $TeamDeployedCount[%i, T1AmmoDeployable] = 0; 
      $TeamDeployedCount[%i, T1MineDeployed] = 0;
      $TeamDeployedCount[%i, T1PulseSensorDeployable] = 0;
      $TeamDeployedCount[%i, T1MotionSensorDeployable] = 0;
      $TeamDeployedCount[%i, TargetBeacon] = 0;
      $TeamDeployedCount[%i, MarkerBeacon] = 0; 
      $TeamDeployedCount[%i, T1DeployedCamera] = 0;
   }
   if(!lightMaleHumanArmor.starsiegeData){
      loadRetInfo(1);// load ret and weapon info fail safe for single player
   }
   if($T1OnlyWeapons){
      if(!$InvyBanDef){
         $InvBanDef["CTF", "ShockLance"] =  $InvBanList["CTF", "ShockLance"];
         $InvBanDef["CTF", "MissileLauncher"] =  $InvBanList["CTF", "MissileLauncher"];

         $InvBanDef["CTF", "CloakingPack"] =  $InvBanList["CTF", "CloakingPack"];
         $InvBanDef["CTF", "SatchelCharge"] =  $InvBanList["CTF", "SatchelCharge"];
         $InvBanDef["CTF", "ELFBarrelPack"] =  $InvBanList["CTF", "ELFBarrelPack"];
         $InvBanDef["CTF", "MortarBarrelPack"] =  $InvBanList["CTF", "MortarBarrelPack"];
         $InvBanDef["CTF", "PlasmaBarrelPack"] =  $InvBanList["CTF", "PlasmaBarrelPack"];
         $InvBanDef["CTF", "AABarrelPack"] =  $InvBanList["CTF", "AABarrelPack"];
         $InvBanDef["CTF", "MissileBarrelPack"] =  $InvBanList["CTF", "MissileBarrelPack"];

         $InvBanDef["CTF", "FlashGrenade"] =  $InvBanList["CTF", "FlashGrenade"];
         $InvBanDef["CTF", "ConcussionGrenade"] =  $InvBanList["CTF", "ConcussionGrenade"];
         $InvBanDef["CTF", "FlareGrenade"] =  $InvBanList["CTF", "FlareGrenade"];
         $InvyBanDef = 1;
      }

      $InvBanList["CTF", "ShockLance"] = 1;
      $InvBanList["CTF", "MissileLauncher"] = 1;

      $InvBanList["CTF", "CloakingPack"] = 1;
      $InvBanList["CTF", "SatchelCharge"] = 1;
      $InvBanList["CTF", "ELFBarrelPack"] = 1;
      $InvBanList["CTF", "MortarBarrelPack"] = 1;
      $InvBanList["CTF", "PlasmaBarrelPack"] = 1;
      $InvBanList["CTF", "AABarrelPack"] = 1;
      $InvBanList["CTF", "MissileBarrelPack"] = 1;

      $InvBanList["CTF", "FlashGrenade"] = 1;
      $InvBanList["CTF", "ConcussionGrenade"] = 1;
      $InvBanList["CTF", "FlareGrenade"] = 1;
   }
}
function T1StartObj::onRemove(%this, %obj){  
   Parent::onRemove(%this, %obj);
   if($T1OnlyWeapons){
      $InvBanList["CTF", "ShockLance"] =  $InvBanDef["CTF", "ShockLance"];
      $InvBanList["CTF", "MissileLauncher"] =  $InvBanDef["CTF", "MissileLauncher"];

      $InvBanList["CTF", "CloakingPack"] =  $InvBanDef["CTF", "CloakingPack"];
      $InvBanList["CTF", "SatchelCharge"] =  $InvBanDef["CTF", "SatchelCharge"];
      $InvBanList["CTF", "ELFBarrelPack"] =  $InvBanDef["CTF", "ELFBarrelPack"];
      $InvBanList["CTF", "MortarBarrelPack"] =  $InvBanDef["CTF", "MortarBarrelPack"];
      $InvBanList["CTF", "PlasmaBarrelPack"] =  $InvBanDef["CTF", "PlasmaBarrelPack"];
      $InvBanList["CTF", "AABarrelPack"] =  $InvBanDef["CTF", "AABarrelPack"];
      $InvBanList["CTF", "MissileBarrelPack"] =  $InvBanDef["CTF", "MissileBarrelPack"];

      $InvBanList["CTF", "FlashGrenade"] =  $InvBanDef["CTF", "FlashGrenade"];
      $InvBanList["CTF", "ConcussionGrenade"] =  $InvBanDef["CTF", "ConcussionGrenade"];
      $InvBanList["CTF", "FlareGrenade"] =  $InvBanDef["CTF", "FlareGrenade"];   
   }
}

function T1WeaponImage::onMount(%this,%obj,%slot)
{
   //MES -- is call below useful at all?
   //Parent::onMount(%this, %obj, %slot);
   if(%obj.getClassName() !$= "Player")
      return;

   //messageClient(%obj.client, 'MsgWeaponMount', "", %this, %obj, %slot);
   // Looks arm position
   if (%this.armthread $= "")
   {
      %obj.setArmThread(look);
   }
   else
   {
      %obj.setArmThread(%this.armThread);
   }
   
   // Initial ammo state
   if(%obj.getMountedImage($WeaponSlot).ammo !$= "")
      if (%obj.getInventory(%this.ammo))
         %obj.setImageAmmo(%slot,true);

   %shape = %this.shapeFile; 
   switch$(%shape){//    t1RepairPackGun.dts
      case "t1plasma.dts":
         %obj.client.setWeaponsHudActive("Blaster");
      case "t1Chaingun.dts":
         %obj.client.setWeaponsHudActive("Chaingun");
      case "t1disc.dts":
         %obj.client.setWeaponsHudActive("Disc");
      case "T1ELF.dts":
         %obj.client.setWeaponsHudActive("ELFGun");
      case "t1GrenadeLauncher.dts":
         %obj.client.setWeaponsHudActive("GrenadeLauncher");
      case "t1mortar.dts":
         %obj.client.setWeaponsHudActive("Mortar");
      case "t1plasma.dts":
         %obj.client.setWeaponsHudActive("Plasma");
      case "t1sniper.dts":
         %obj.client.setWeaponsHudActive("SniperRifle");
      case "t1TargetLaser.dts":
         %obj.client.setWeaponsHudActive("TargetingLaser");
      default:
         %obj.client.setWeaponsHudActive("Blaster");
   }
   if(%obj.getMountedImage($WeaponSlot).ammo !$= "")
      %obj.client.setAmmoHudCount(%obj.getInventory(%this.ammo));
   else
      %obj.client.setAmmoHudCount(-1);
}

function T1WeaponImage::onUnmount(%this,%obj,%slot)
{
   %obj.client.setWeaponsHudActive(%this.item, 1);
   %obj.client.setAmmoHudCount(-1);
   commandToClient(%obj.client,'removeReticle');
   // try to avoid running around with sniper/missile arm thread and no weapon
   %obj.setArmThread(look);
   Parent::onUnmount(%this, %obj, %slot);
}

function giveDisc(){
   %player = LocalClientConnection.player; 
   %player.setInventory("T1Disc", 1, true);
   %player.setInventory("DiscAmmo", 15, true);
}


datablock AudioProfile(T1RainSfx)
{
   filename    = "t1sounds/rain.wav";
   description = AudioLooping2d;
};

datablock PrecipitationData(T1Rain)
{
   type = 0;
   soundProfile = "T1RainSfx";
   materialList = "raindrops.dml";
   sizeX = 0.2;
   sizeY = 0.45;

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

datablock AudioProfile(T1DiscSwitchSound)
{
   filename    = "t1sounds/Pku_weap.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(T1DiscLoopSound)
{
   filename    = "t1sounds/DISCLOOP.wav";
   description = ClosestLooping3d;
};


datablock AudioProfile(T1DiscFireSound)
{
   filename    = "t1sounds/rocket2.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(T1DiscReloadSound)
{
   filename    = "t1sounds/discreload.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(T1DiscExpSound)
{
   filename    = "t1sounds/rockexp.wav";
   description = AudioExplosion3d;
   preload = true;
};

//datablock AudioProfile(discProjectileSound)
//{
   //filename    = "fx/weapons/spinfusor_projectile.wav";
   //description = ProjectileLooping3d;
   //preload = true;
//};
//
//datablock AudioProfile(DiscDryFireSound)
//{
   //filename    = "fx/weapons/spinfusor_dryfire.wav";
   //description = AudioClose3d;
   //preload = true;
//};
datablock ExplosionData(T1DiscExplosion)
{
   explosionShape = "disc_explosion.dts";
   soundProfile   = T1DiscExpSound;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;

   sizes[0] = "1.0 1.0 1.0";
   sizes[1] = "1.0 1.0 1.0";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock LinearProjectileData(T1DiscProjectile)
{
   projectileShapeName = "disc.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.50;
   damageRadius        = 7.5;
   radiusDamageType    = $DamageType::Disc;
   kickBackStrength    = 2000;  // z0dd - ZOD, 4/24/02. Was 1750

   sound 				= discProjectileSound;
   explosion           = "T1DiscExplosion";
   underwaterExplosion = "UnderwaterDiscExplosion";
   splash              = DiscSplash;

   dryVelocity       = 95; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 90
   wetVelocity       = 55; // z0dd - ZOD, 3/30/02. Slight disc speed up. was 50
   velInheritFactor  = 0.75; // z0dd - ZOD, 3/30/02. was 0.5
   fizzleTimeMS      = 5000;
   lifetimeMS        = 5000;
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 20.0; // z0dd - ZOD, 4/24/02. Was 0.0. 20 degrees skips off water
   fizzleUnderwaterMS        = 5000;

   activateDelayMS = 200;

   hasLight    = true;
   lightRadius = 6.0;
   lightColor  = "0.175 0.175 0.5";
};


datablock ItemData(T1Disc){
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "t1disc.dts";
   image = T1DiscImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   pickUpName = "a T1 disc";
   ammoAmount = 15;
   ammo = T1DiscAmmo;
};


datablock ItemData(T1DiscAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_disc.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some spinfusor discs";
   computeCRC = true; // z0dd - ZOD, 5/19/03. Was missing this param
};


datablock ShapeBaseImageData(T1DiscImage){   
   className = T1WeaponImage;
   shapeFile = "t1disc.dts";
   item = T1Disc;
   ammo = T1DiscAmmo;
   offset = "0 0 0";
   emap = true;

   //projectileSpread = 1.0 / 1000.0; // z0dd - ZOD, 4/14/02. No disc spread.

   projectile = T1DiscProjectile;
   projectileType = LinearProjectile;

   // State Data
   stateName[0]                     = "Preactivate";
   stateTransitionOnLoaded[0]       = "Activate";
   stateTransitionOnNoAmmo[0]       = "NoAmmo";

   stateName[1]                     = "Activate";
   stateTransitionOnTimeout[1]      = "Ready";
   stateTimeoutValue[1]             = 0.5;
   stateSequence[1]                 = "Activated";
   stateSound[1]                    = T1DiscSwitchSound;

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";
   stateSequence[2]                 = "DiscSpin";
   stateSound[2]                    = T1DiscLoopSound;

   stateName[3]                     = "Fire";
   stateTransitionOnTimeout[3]      = "Reload";
   stateTimeoutValue[3]             = 1.25;
   stateFire[3]                     = true;
   stateRecoil[3]                   = LightRecoil;
   stateAllowImageChange[3]         = false;
   stateSequence[3]                 = "Fire";
   stateScript[3]                   = "onFire";
   stateSound[3]                    = T1DiscFireSound;

   stateName[4]                     = "Reload";
   stateTransitionOnNoAmmo[4]       = "NoAmmo";
   stateTransitionOnTimeout[4]      = "Ready";
   stateTimeoutValue[4]             = 0.5; // 0.25 load, 0.25 spinup
   stateAllowImageChange[4]         = false;
   stateSequence[4]                 = "Reload";
   stateSound[4]                    = T1DiscReloadSound;

   stateName[5]                     = "NoAmmo";
   stateTransitionOnAmmo[5]         = "Reload";
   stateSequence[5]                 = "NoAmmo";
   stateTransitionOnTriggerDown[5]  = "DryFire";

   stateName[6]                     = "DryFire";
   stateSound[6]                    = DiscDryFireSound;
   stateTimeoutValue[6]             = 1.0;
   stateTransitionOnTimeout[6]      = "NoAmmo";
};

function T1DiscImage::onMount(%this,%obj,%slot){ 
   Parent::onMount(%this, %obj, %slot);
}

function T1DiscImage::onUnmount(%this,%obj,%slot){
   Parent::onUnmount(%this, %obj, %slot);

}



//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
datablock AudioProfile(T1TargetingLaserPaintSound)
{
   filename    = "t1sounds/tgt_laser.wav";
   description = CloseLooping3d;
   preload = true;
};

datablock ItemData(T1TargetingLaser)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "t1TargetLaser.dts";
   image        = T1TargetingLaserImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;

};

datablock TargetProjectileData(t1BasicTargeter)
{
   directDamage        	= 0.0;
   hasDamageRadius     	= false;
   indirectDamage      	= 0.0;
   damageRadius        	= 0.0;
   velInheritFactor    	= 1.0;

   maxRifleRange       	= 1000;
   beamColor           	= "0.1 1.0 0.1";
								
   startBeamWidth			= 0.05;
   pulseBeamWidth 	   = 0.05;
   beamFlareAngle 	   = 3.0;
   minFlareSize        	= 0.0;
   maxFlareSize        	= 400.0;
   pulseSpeed          	= 6.0;
   pulseLength         	= 0.150;

   textureName[0]      	= "special/nonlingradient";
   textureName[1]      	= "special/flare";
   textureName[2]      	= "special/pulse";
   textureName[3]      	= "special/expFlare";
   beacon               = true;
};


datablock ShapeBaseImageData(T1TargetingLaserImage)
{
   className = T1WeaponImage;

   shapeFile = "t1TargetLaser.dts";
   item = TargetingLaser;
   offset = "0 0 0";

   projectile = t1BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = T1DiscSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateSequence[3]                 = "Fire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = T1TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateSequence[5] = "activate";
   stateTransitionOnTimeout[5]      = "Ready";
};

function T1TargetingLaserImage::onMount(%this,%obj,%slot){ 
   Parent::onMount(%this, %obj, %slot);
}

function T1TargetingLaserImage::onUnmount(%this,%obj,%slot){
   Parent::onUnmount(%this, %obj, %slot);
}
function TargetingLaser::onUse(%data, %obj){
    if(Game.weaponOnUse(%data, %obj)){
       if (%obj.getDataBlock().className $= Armor){
         if(isObject(StarsiegeTribesMap) && (StarsiegeTribesMap.teamOnly == %player.team || !StarsiegeTribesMap.teamOnly)){
            %obj.mountImage(T1TargetingLaserImage, $WeaponSlot);
         }
         else{
            %obj.mountImage(TargetingLaserImage, $WeaponSlot);  
         }
       }
    }
 }
 
 function TargetingLaser::onThrow(%data,%obj,%plr){
   //parent::onThrow(%data,%obj,%plr);
   if(isObject(%obj) && isObject(StarsiegeTribesMap)){
      %obj.setDataBlock(T1TargetingLaser);
      %plr.unmountImage($WeaponSlot);
   }
 }

function giveTargetLaser(){
   %player = LocalClientConnection.player; 
   %player.setInventory("T1TargetingLaser", 1, true);
}

function T1TargetingLaserImage::onFire(%data,%obj,%slot)
{
   %p = Parent::onFire(%data, %obj, %slot);
   //--------------------------------------------------------
   // z0dd - ZOD, 9/3/02. Anti rapid fire mortar/missile fix.
   if(!%p)
   {
      return;	
   }
   //--------------------------------------------------------
   %p.setTarget(%obj.team);
}

////////////////////////////////////////////////////////////////////////////////////////////////
datablock ItemData(T1ELF){
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "T1ELF.dts";
   image = T1ELFImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   pickUpName = "a T1 disc";
};
datablock ELFProjectileData(T1BasicELF)
{
   beamRange         = 38; // z0dd - ZOD, 5/18/02. WHAT?? INCREASE ELF RANGE?!!? was 37
   numControlPoints  = 8;
   restorativeFactor = 3.75;
   dragFactor        = 4.5;
   endFactor         = 2.25;
   randForceFactor   = 2;
   randForceTime     = 0.125;
	drainEnergy			= 1.0;
	drainHealth			= 0.01;
   directDamageType  = $DamageType::ELF;
	mainBeamWidth     = 0.1;           // width of blue wave beam
	mainBeamSpeed     = 9.0;            // speed that the beam travels forward
	mainBeamRepeat    = 0.25;           // number of times the texture repeats
   lightningWidth    = 0.1;
   lightningDist      = 0.15;           // distance of lightning from main beam

   fireSound    = ElfGunFireSound;
   wetFireSound = ElfFireWetSound;

	textures[0] = "special/ELFBeam";
   textures[1] = "special/ELFLightning";
   textures[2] = "special/BlueImpact";

   emitter = ELFSparksEmitter;
};

datablock ShapeBaseImageData(T1ELFImage){   
   className = T1WeaponImage;
   shapeFile = "T1ELF.dts";
   item = T1ELF;
   offset = "0 0 0";
   emap = true;

   projectile = T1BasicELF;
   projectileType = ELFProjectile;
   deleteLastProjectile = true;
   emap = true;
   

	usesEnergy = true;
 	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = ELFGunSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "CheckWet";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 5;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   //stateSound[3]                    = ElfFireWetSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
   stateTransitionOnNoAmmo[6]       = "NoAmmo";
   
   stateName[6]                     = "DryFire";
   stateSound[6]                    = ElfFireWetSound;
   stateTimeoutValue[6]             = 0.5;
   stateTransitionOnTimeout[6]      = "Ready";
   
   stateName[7]                     = "CheckWet";
   stateTransitionOnWet[7]          = "DryFire";
   stateTransitionOnNotWet[7]       = "Fire";
};

function T1ELFImage::onFire(%data, %obj, %slot)
{
   %p = Parent::onFire(%data, %obj, %slot);
   //--------------------------------------------------------
   // z0dd - ZOD, 9/3/02. Anti rapid fire mortar/missile fix.
   if(!%p)
   {
      return;	
   }
   //--------------------------------------------------------
   if(!%p.hasTarget())
      %obj.playAudio(0, ELFFireWetSound);
}

function T1ELFImage::onMount(%this,%obj,%slot){ 
   Parent::onMount(%this, %obj, %slot); 
}

function T1ELFImage::onUnmount(%this,%obj,%slot){
   Parent::onUnmount(%this, %obj, %slot);
}

function giveELF(){
   %player = LocalClientConnection.player; 
   %player.setInventory("T1ELF", 1, true);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

datablock AudioProfile(T1SniperFireSound)
{
   filename    = "t1sounds/sniper.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(T1SniperExpSound)
{
   filename    = "t1sounds/laserhit.wav";
   description = AudioExplosion3d;
   preload = true;
};
datablock ExplosionData(T1SniperExplosion)
{
   explosionShape = "energy_explosion.dts";
   soundProfile   = T1SniperExpSound;

   particleEmitter = SniperExplosionEmitter;
   particleDensity = 150;
   particleRadius = 0.25;

   faceViewer           = false;
};


//--------------------------------------
// Projectile
//--------------------------------------
datablock SniperProjectileData(T1BasicSniperShot)
{
   directDamage        = 0.4;
   hasDamageRadius     = false;
   indirectDamage      = 0.0;
   damageRadius        = 0.0;
   velInheritFactor    = 1.0;
   sound 				  = "";
   explosion           = "T1SniperExplosion";
   splash              = SniperSplash;
   directDamageType    = $DamageType::Laser;

   maxRifleRange       = 1000;
   rifleHeadMultiplier = 1.3;
   beamColor           = "1 0.1 0.1";
   fadeTime            = 1.0;

   startBeamWidth		  = 0.145;
   endBeamWidth 	     = 0.25;
   pulseBeamWidth 	  = 0.5;
   beamFlareAngle 	  = 3.0;
   minFlareSize        = 0.0;
   maxFlareSize        = 400.0;
   pulseSpeed          = 6.0;
   pulseLength         = 0.150;

   lightRadius         = 1.0;
   lightColor          = "0.3 0.0 0.0";

   textureName[0]      = "special/flare";
   textureName[1]      = "special/nonlingradient";
   textureName[2]      = "special/laserrip01";
   textureName[3]      = "special/laserrip02";
   textureName[4]      = "special/laserrip03";
   textureName[5]      = "special/laserrip04";
   textureName[6]      = "special/laserrip05";
   textureName[7]      = "special/laserrip06";
   textureName[8]      = "special/laserrip07";
   textureName[9]      = "special/laserrip08";
   textureName[10]     = "special/laserrip09";
   textureName[11]     = "special/sniper00";

};

datablock ItemData(T1Sniper){
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "t1sniper.dts";
   image = T1SniperImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   pickUpName = "a T1 disc";
};

datablock ShapeBaseImageData(T1SniperImage){   
   className = T1WeaponImage;
   shapeFile = "t1sniper.dts";
   item = T1Sniper;
   //ammo = PlasmaAmmo;
   offset = "0 0 0";
   emap = true;

   projectile = T1BasicSniperShot;
   projectileType = SniperProjectile;
  // armThread = looksn;
   // z0dd - ZOD, 5/07/04. Use ammo if gameplay changes in affect
   usesEnergy = $Host::ClassicLoadSniperChanges ? false : true;
   minEnergy = 6;

   stateName[0]                     = "Activate";
   stateTransitionOnTimeout[0]      = "ActivateReady";
   stateSound[0]                    = SniperRifleSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateSequence[0]                 = "Activate";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnLoaded[1]       = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "CheckWet";

   stateName[3]                     = "Fire";
   stateTransitionOnTimeout[3]      = "Reload";
   stateTimeoutValue[3]             = 0.5;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateSequence[3]                 = "Fire";
   stateScript[3]                   = "onFire";

   stateName[4]                     = "Reload";
   stateTransitionOnTimeout[4]      = "Ready";
   stateTimeoutValue[4]             = 0.5;
   stateAllowImageChange[4]         = false;

   stateName[5]                     = "CheckWet";
   stateTransitionOnWet[5]          = "DryFire";
   stateTransitionOnNotWet[5]       = "Fire";
   
   stateName[6]                     = "NoAmmo";
   stateTransitionOnAmmo[6]         = "Reload";
   stateTransitionOnTriggerDown[6]  = "DryFire";
   stateSequence[6]                 = "NoAmmo";
   
   stateName[7]                     = "DryFire";
   stateSound[7]                    = SniperRifleDryFireSound;
   stateTimeoutValue[7]             = 0.5;
   stateTransitionOnTimeout[7]      = "Ready";
};
function T1SniperImage::onFire(%data,%obj,%slot)
{
   if(!%obj.hasEnergyPack || %obj.getEnergyLevel() < %this.minEnergy) // z0dd - ZOD, 5/22/03. Check for energy too.
   {
      // siddown Junior, you can't use it
      serverPlay3D(SniperRifleDryFireSound, %obj.getTransform());
      return;
   }

   %pct = %obj.getEnergyLevel() / %obj.getDataBlock().maxEnergy;
   %p = new (%data.projectileType)() {
      dataBlock        = %data.projectile;
      initialDirection = %obj.getMuzzleVector(%slot);
      initialPosition  = %obj.getMuzzlePoint(%slot);
      sourceObject     = %obj;
      damageFactor     = %pct * %pct;
      sourceSlot       = %slot;
   };
   %p.setEnergyPercentage(%pct);

   %obj.lastProjectile = %p;
   MissionCleanup.add(%p);
   serverPlay3D(T1SniperFireSound, %obj.getTransform());
   
   // AI hook
   if(%obj.client)
      %obj.client.projectile = %p;

   %obj.setEnergyLevel(0);
   if($Host::ClassicLoadSniperChanges)
      %obj.decInventory(%data.ammo, 1);
}

function T1SniperImage::onMount(%this,%obj,%slot){ 
   Parent::onMount(%this, %obj, %slot);
}

function T1SniperImage::onUnmount(%this,%obj,%slot){
   Parent::onUnmount(%this, %obj, %slot);
}

function giveSniper(){ 
   %player = LocalClientConnection.player; 
   %player.setInventory("T1Sniper", 1, true);
   //%player.setInventory("PlasmaAmmo", 25, true);
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

datablock AudioProfile(T1BlasterFireSound)
{
   filename    = "t1sounds/rifle1.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(T1BlasterExpSound)
{
   filename    = "t1sounds/energyexp.wav";
   description = AudioExplosion3d;
   preload = true;
};


datablock ExplosionData(T1BlasterExplosion)
{
   soundProfile   = T1BlasterExpSound;
   emitter[0]     = BlasterExplosionEmitter;
   emitter[1]     = BlasterExplosionEmitter2;
};

datablock EnergyProjectileData(T1EnergyBolt)
{
   emitterDelay        = -1;
   // z0dd - ZOD, 5/07/04. Less damage shotgun blaster is gameplay changes in affect
   directDamage        = $Host::ClassicLoadBlasterChanges ? 0.05 : 0.15;
   directDamageType    = $DamageType::Blaster;
   kickBackStrength    = 0.0;
   bubbleEmitTime      = 1.0;
   // z0dd - ZOD, 5/07/04. Shotgun blaster no sound when gameplay changes in affect
   sound = "";
   velInheritFactor    = 0.5;

   explosion           = "T1BlasterExplosion";
   splash              = BlasterSplash;


   grenadeElasticity = 0.998;
   grenadeFriction   = 0.0;
   armingDelayMS     = 500;

   muzzleVelocity    = 90.0;

   drag = 0.05;

   gravityMod        = 0.0;

   dryVelocity       = 200.0;
   wetVelocity       = 150.0;

   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0.5 0.175 0.175";

   scale = "0.25 20.0 1.0";
   crossViewAng = 0.99;
   crossSize = 0.55;

   lifetimeMS     = 3000;
   blurLifetime   = 0.2;
   blurWidth      = 0.25;
   blurColor = "0.4 0.0 0.0 1.0";

   texture[0] = "special/blasterBolt";
   texture[1] = "special/blasterBoltCross";
};


datablock ItemData(T1Blaster){
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "t1blaster.dts";
   image = T1BlasterImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   pickUpName = "a T1 disc";
};

datablock ShapeBaseImageData(T1BlasterImage){   
   className = T1WeaponImage;
   shapeFile = "t1blaster.dts";
   item = T1Blaster;
   //ammo = PlasmaAmmo;
   offset = "0 0 0";
   emap = true;

   projectile = T1EnergyBolt;
   projectileType = EnergyProjectile;

   usesEnergy = true;
   // z0dd - ZOD, 5/07/04. More drain shotgun blaster is gameplay changes in affect
   fireEnergy = $Host::ClassicLoadBlasterChanges ? 8 : 4;
   minEnergy = $Host::ClassicLoadBlasterChanges ? 8 : 4;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = T1DiscSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateSequence[2] = "Activate";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.3;
   stateFire[3] = true;
   stateRecoil[3] = NoRecoil;
   stateAllowImageChange[3] = false;
   //stateSequence[3] = "Fire";
   stateSound[3] = T1BlasterFireSound;
   stateScript[3] = "onFire";

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateAllowImageChange[4] = false;
   //stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   //stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";
   
   stateName[6] = "DryFire";
   stateTimeoutValue[6] = 0.3;
   stateSound[6] = BlasterDryFireSound;
   stateTransitionOnTimeout[6] = "Ready";
};

function T1BlasterImage::onMount(%this,%obj,%slot){ 
   Parent::onMount(%this, %obj, %slot);
}

function T1BlasterImage::onUnmount(%this,%obj,%slot){
   Parent::onUnmount(%this, %obj, %slot);
}

function giveBlaster(){
   %player = LocalClientConnection.player; 
   %player.setInventory("T1Blaster", 1, true);
   //%player.setInventory("PlasmaAmmo", 25, true);
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

datablock AudioProfile(T1PlasmaFireSound)
{
   filename    = "t1sounds/Plasma2.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(T1plasmaExpSound)
{
   filename    = "t1sounds/Explo4.wav";
   description = AudioExplosion3d;
   preload = true;
};
datablock ExplosionData(T1PlasmaBoltExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = T1plasmaExpSound;
   particleEmitter = PlasmaExplosionEmitter;
   particleDensity = 150;
   particleRadius = 1.25;
   faceViewer = true;

   sizes[0] = "1.0 1.0 1.0";
   sizes[1] = "1.0 1.0 1.0";
   times[0] = 0.0;
   times[1] = 1.5;
};


datablock LinearFlareProjectileData(T1PlasmaBolt)
{
   projectileShapeName = "plasmabolt.dts";
   scale               = "2.0 2.0 2.0";
   faceViewer          = true;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.45;
   damageRadius        = 4.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::Plasma;

   explosion           = "T1PlasmaBoltExplosion";
   splash              = PlasmaSplash;

   dryVelocity       = 70.0; // z0dd - ZOD, 7/20/02. Faster plasma projectile. was 55
   wetVelocity       = -1;
   velInheritFactor  = 0.3;
   fizzleTimeMS      = 2000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
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




datablock ItemData(T1Plasma){
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "t1plasma.dts";
   image = T1PlasmaImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   pickUpName = "a T1 disc";
   ammoAmount = 30;
   ammo = T1PlasmaAmmo;
};

datablock ItemData(T1PlasmaAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_disc.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some plasma rounds";
   computeCRC = true; // z0dd - ZOD, 5/19/03. Was missing this param
};

datablock ShapeBaseImageData(T1PlasmaImage){   
   className = T1WeaponImage;
   shapeFile = "t1plasma.dts";
   item = T1Plasma;
   ammo = T1PlasmaAmmo;
   offset = "0 0 0";
   emap = true;

    projectile = T1PlasmaBolt;
   projectileType = LinearFlareProjectile;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = T1DiscSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "CheckWet";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.1;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateSequence[3] = "Fire";
   stateEmitterTime[3] = 0.2;
   stateSound[3] = T1PlasmaFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.6;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   //stateSound[4] = PlasmaReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = PlasmaDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
   
   stateName[7]       = "WetFire";
   stateSound[7]      = PlasmaFireWetSound;
   stateTimeoutValue[7]        = 1.5;
   stateTransitionOnTimeout[7] = "Ready";
   
   stateName[8]               = "CheckWet";
   stateTransitionOnWet[8]    = "WetFire";
   stateTransitionOnNotWet[8] = "Fire"; 
};

function T1PlasmaImage::onMount(%this,%obj,%slot){ 
   Parent::onMount(%this, %obj, %slot);
}

function T1PlasmaImage::onUnmount(%this,%obj,%slot){
   Parent::onUnmount(%this, %obj, %slot);
}

function givePlasma(){
   %player = LocalClientConnection.player; 
   %player.setInventory("T1Plasma", 1, true);
   %player.setInventory("PlasmaAmmo", 25, true);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

datablock AudioProfile(T1MortarFireSound)
{
   filename    = "t1sounds/mortar_fire.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(T1MortarExpSound)
{
   filename    = "t1sounds/shockexp.wav";
   description = AudioExplosion3d;
   preload = true;
};

datablock AudioProfile(T1MortarReloadSound)
{
   filename    = "t1sounds/Mortar_reload.wav";
   description = AudioExplosion3d;
   preload = true;
};

datablock AudioProfile(T1MortarIdleSound)
{
   filename    = "t1sounds/mortar_idle.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};

datablock ItemData(T1Mortar){
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "t1mortar.dts"; 
   image = T1MortarImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   pickUpName = "a T1 disc";
   ammoAmount = 10;
   ammo = T1MortarAmmo;
};

datablock ItemData(T1MortarAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_disc.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some mortar rounds";
   computeCRC = true; // z0dd - ZOD, 5/19/03. Was missing this param
};


datablock ExplosionData(T1MortarExplosion)
{
   soundProfile   = T1MortarExpSound;

   shockwave = MortarShockwave;
   shockwaveOnTerrain = true;

   subExplosion[0] = MortarSubExplosion1;
   subExplosion[1] = MortarSubExplosion2;
   subExplosion[2] = MortarSubExplosion3;

   emitter[0] = MortarExplosionSmokeEmitter;
   emitter[1] = MortarCrescentEmitter;

   shakeCamera = true;
   camShakeFreq = "8.0 9.0 7.0";
   camShakeAmp = "100.0 100.0 100.0";
   camShakeDuration = 1.3;
   camShakeRadius = 25.0;
};

datablock GrenadeProjectileData(T1MortarShot)
{
   projectileShapeName = "mortar_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 1.0;
   damageRadius        = 19.0; // z0dd - ZOD, 8/13/02. Was 20.0
   radiusDamageType    = $DamageType::Mortar;
   kickBackStrength    = 2500;

   explosion           = "T1MortarExplosion";
   underwaterExplosion = "UnderwaterMortarExplosion";
   velInheritFactor    = 0.5;
   splash              = MortarSplash;
   depthTolerance      = 10.0; // depth at which it uses underwater explosion
   
   baseEmitter         = MortarSmokeEmitter;
   bubbleEmitter       = MortarBubbleEmitter;
   
   grenadeElasticity = 0.15;
   grenadeFriction   = 0.4;
   armingDelayMS     = 1500; // z0dd - ZOD, 4/14/02. Was 2000

   gravityMod        = 1.1;  // z0dd - ZOD, 5/18/02. Make mortar projectile heavier, less floaty
   muzzleVelocity    = 75.95; // z0dd - ZOD, 8/13/02. More velocity to compensate for higher gravity. Was 63.7
   drag              = 0.1;
   sound	     = MortarProjectileSound;

   hasLight    = true;
   lightRadius = 4;
   lightColor  = "0.05 0.2 0.05";

   hasLightUnderwaterColor = true;
   underWaterLightColor = "0.05 0.075 0.2";

   // z0dd - ZOD, 5/22/03. Limit the mortars effective range to around 450 meters.
   // Addresses long range mortar spam exploit.
   explodeOnDeath = $Host::ClassicLoadMortarChanges ? true : false;
   fizzleTimeMS = $Host::ClassicLoadMortarChanges ? 8000 : -1;
   lifetimeMS = $Host::ClassicLoadMortarChanges ? 10000 : -1;
   fizzleUnderwaterMS = $Host::ClassicLoadMortarChanges ? 8000 : -1;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact = false;
   deflectionOnWaterImpact = 0.0;
};

datablock ShapeBaseImageData(T1MortarImage){   
   className = T1WeaponImage;
   shapeFile = "t1mortar.dts";
   item = T1Mortar;
   ammo = T1MortarAmmo;
   offset = "0 0 0";
   emap = true;

   projectile = T1MortarShot;
   projectileType = GrenadeProjectile;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = T1DiscSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnAmmo[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "FirstLoad";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";
   stateSound[2] = T1MortarIdleSound;

   stateName[3] = "Fire";
   stateSequence[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.8;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateScript[3] = "onFire";
   stateSound[3] = T1MortarFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 2.0;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   stateSound[4] = T1MortarReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = MortarDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
   
   stateName[7] = "FirstLoad";
   stateTransitionOnAmmo[7] = "Ready";
};

function T1MortarImage::onMount(%this,%obj,%slot){ 
   Parent::onMount(%this, %obj, %slot);
}

function T1MortarImage::onUnmount(%this,%obj,%slot){
   Parent::onUnmount(%this, %obj, %slot);
}

function giveMortar(){
   %player = LocalClientConnection.player; 
   %player.setInventory("T1Mortar", 1, true);
   %player.setInventory("MortarAmmo", 25, true);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

datablock AudioProfile(T1GLFireSound)
{
   filename    = "t1sounds/Grenade.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(T1GLExpSound)
{
   filename    = "t1sounds/BXplo3.wav";
   description = AudioExplosion3d;
   preload = true;
};

datablock AudioProfile(T1GLReloadSound)
{
   filename    = "t1sounds/Dryfire1.wav";
   description = AudioExplosion3d;
   preload = true;
};


datablock ExplosionData(T1GrenadeExplosion)
{
   soundProfile   = T1GLExpSound;

   faceViewer           = true;
   explosionScale = "0.8 0.8 0.8";

   debris = GrenadeDebris;
   debrisThetaMin = 10;
   debrisThetaMax = 50;
   debrisNum = 8;
   debrisVelocity = 26.0;
   debrisVelocityVariance = 7.0;

   emitter[0] = GrenadeDustEmitter;
   emitter[1] = GExplosionSmokeEmitter;
   emitter[2] = GrenadeSparksEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 6.0 9.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 20.0;
};

datablock GrenadeProjectileData(T1BasicGrenade)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.40;
   damageRadius        = 15.0;
   radiusDamageType    = $DamageType::Grenade;
   kickBackStrength    = 1500;
   bubbleEmitTime      = 1.0;

   sound               = GrenadeProjectileSound;
   explosion           = "T1GrenadeExplosion";
   underwaterExplosion = "UnderwaterGrenadeExplosion";
   velInheritFactor    = 0.85; // z0dd - ZOD, 3/30/02. Was 0.5
   splash              = GrenadeSplash;

   baseEmitter         = GrenadeSmokeEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;

   grenadeElasticity = 0.30; // z0dd - ZOD, 9/13/02. Was 0.35
   grenadeFriction   = 0.2;
   armingDelayMS     = 650; // z0dd - ZOD, 9/13/02. Was 1000
   muzzleVelocity    = 75.00; // z0dd - ZOD, 3/30/02. GL projectile is faster. Was 47.00
   //drag = 0.1; // z0dd - ZOD, 3/30/02. No drag.
   gravityMod        = 1.9; // z0dd - ZOD, 5/18/02. Make GL projectile heavier, less floaty
};

datablock ItemData(T1GL){
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "t1GrenadeLauncher.dts"; 
   image = T1GLImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   pickUpName = "a T1 disc";
   ammoAmount = 10;
   ammo = T1GrenadeLauncherAmmo;
};

datablock ItemData(T1GrenadeLauncherAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_disc.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some grenade launcher rounds";
   computeCRC = true; // z0dd - ZOD, 5/19/03. Was missing this param
};


datablock ShapeBaseImageData(T1GLImage){   
   className = T1WeaponImage;
   shapeFile = "t1GrenadeLauncher.dts";
   item = T1GL;
   ammo = T1GrenadeLauncherAmmo;
   offset = "0 0 0";
   emap = true;

   projectile = T1BasicGrenade;
   projectileType = GrenadeProjectile;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = T1DiscSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.4;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateScript[3] = "onFire";
   stateSound[3] = T1GLFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.5;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   stateSound[4] = T1GLReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = GrenadeDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
};

function T1GLImage::onMount(%this,%obj,%slot){ 
   Parent::onMount(%this, %obj, %slot);
}

function T1GLImage::onUnmount(%this,%obj,%slot){
   Parent::onUnmount(%this, %obj, %slot);
}

function giveGL(){
   %player = LocalClientConnection.player; 
   %player.setInventory("T1GL", 1, true);
   %player.setInventory("GrenadeLauncherAmmo", 25, true);
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

datablock AudioProfile(T1ChaingunFireSound)
{
   filename    = "t1sounds/machinegun.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};
datablock AudioProfile(T1ChaingunSpinDownSound)
{
   filename    = "t1sounds/Machgun2.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(T1ChaingunSpinUpSound)
{
   filename    = "t1sounds/machgun3.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(T1ChaingunImpact1)
{
   filename    = "t1sounds/Ricoche1.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(T1ChaingunImpact2)
{
   filename    = "t1sounds/Ricoche2.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock AudioProfile(T1ChaingunImpact3)
{
   filename    = "t1sounds/Ricoche3.wav";
   description = AudioClosest3d;
   preload = true;
};
datablock ExplosionData(T1ChaingunExplosion1)
{
   soundProfile   = T1ChaingunImpact1;

   emitter[0] = ChaingunImpactSmoke;
   emitter[1] = ChaingunSparkEmitter;

   faceViewer           = false;
};
datablock ExplosionData(T1ChaingunExplosion2)
{
   soundProfile   = T1ChaingunImpact2;

   emitter[0] = ChaingunImpactSmoke;
   emitter[1] = ChaingunSparkEmitter;

   faceViewer           = false;
};
datablock ExplosionData(T1ChaingunExplosion3)
{
   soundProfile   = T1ChaingunImpact3;

   emitter[0] = ChaingunImpactSmoke;
   emitter[1] = ChaingunSparkEmitter;

   faceViewer           = false;
};

datablock TracerProjectileData(t1ChaingunBullet1)
{
   doDynamicClientHits = true;
   directDamage        = 0.0825;
   directDamageType    = $DamageType::Bullet;
   explosion           = "T1ChaingunExplosion1";
   splash              = ChaingunSplash;
   kickBackStrength  = 0.0;
   sound = ChaingunProjectile;
   dryVelocity       = 750.0; // z0dd - ZOD, 7/12/03. Was 425.0
   wetVelocity       = 280.0; // z0dd - ZOD, 7/12/03. Was 100.0
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;
   tracerLength    = 30.0; // z0dd - ZOD, 7/12/03. Was 15.0
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
   tracerTex[0] = "special/tracer00";
   tracerTex[1] = "special/tracercross";
   tracerWidth  = 0.20; // z0dd - ZOD, 7/12/03. Was 0.10
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;
   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;
};

datablock TracerProjectileData(t1ChaingunBullet2) : t1ChaingunBullet1 { 
   explosion           = "T1ChaingunExplosion2";
};

datablock TracerProjectileData(t1ChaingunBullet3) : t1ChaingunBullet1 { 
   explosion           = "T1ChaingunExplosion3";
}; 

datablock ItemData(T1ChainGun){
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "t1Chaingun.dts"; 
   image = T1ChainGunImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   pickUpName = "a T1 chaingun";
   ammoAmount = 200;
   ammo = T1ChaingunAmmo;
};

datablock ItemData(T1ChaingunAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_disc.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some spinfusor discs";
   computeCRC = true; // z0dd - ZOD, 5/19/03. Was missing this param
};


datablock ShapeBaseImageData(T1ChainGunImage){   
   className = T1WeaponImage;
   shapeFile = "t1Chaingun.dts";

   item = T1ChainGun;
   ammo = T1ChaingunAmmo;
   offset = "0 0.01 0";
   emap = true;

   projectile = t1ChaingunBullet1;
   projectileType = TracerProjectile;
   emap = true;

   casing              = ShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 3.0;

   projectileSpread = 6.0 / 1000.0; // z0dd - ZOD, 7/12/03. Was 8.0

   //--------------------------------------
   stateName[0]             = "Activate";
   stateSequence[0]         = "Activate";
   stateSound[0]            = T1DiscSwitchSound;
   stateAllowImageChange[0] = false;
   //
   stateTimeoutValue[0]        = 0.5;
   stateTransitionOnTimeout[0] = "Ready";
   stateTransitionOnNoAmmo[0]  = "NoAmmo";

   //--------------------------------------
   stateName[1]       = "Ready";
   stateSpinThread[1] = Stop;
   //
   stateTransitionOnTriggerDown[1] = "Spinup";
   stateTransitionOnNoAmmo[1]      = "NoAmmo";
   stateSequence[1]         = "Activate";

   //--------------------------------------
   stateName[2]               = "NoAmmo";
   stateTransitionOnAmmo[2]   = "Ready";
   stateSpinThread[2]         = Stop;
   stateTransitionOnTriggerDown[2] = "DryFire";

   //--------------------------------------
   stateName[3]         = "Spinup";
   stateSpinThread[3]   = SpinUp;
   stateSound[3]        = T1ChaingunSpinupSound;
   //
   stateTimeoutValue[3]          = 0.5;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";

   //--------------------------------------
   stateName[4]             = "Fire";
   stateSequence[4]            = "Fire";
   stateSequenceRandomFlash[4] = true;
   stateSpinThread[4]       = FullSpeed;
   stateSound[4]            = T1ChaingunFireSound;
   //stateRecoil[4]           = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateEjectShell[4]       = true;
   //
   stateTimeoutValue[4]          = 0.15;
   stateTransitionOnTimeout[4]   = "Fire";
   stateTransitionOnTriggerUp[4] = "Spindown";
   stateTransitionOnNoAmmo[4]    = "EmptySpindown";

   //--------------------------------------
   stateName[5]       = "Spindown";
   stateSound[5]      = T1ChaingunSpinDownSound;
   stateSpinThread[5] = SpinDown;
   stateSequence[5]         = "Activate";
   //
   stateTimeoutValue[5]            = 1.0;
   stateWaitForTimeout[5]          = true;
   stateTransitionOnTimeout[5]     = "Ready";
   stateTransitionOnTriggerDown[5] = "Spinup";

   //--------------------------------------
   stateName[6]       = "EmptySpindown";
   stateSound[6]      = T1ChaingunSpinDownSound;
   stateSpinThread[6] = SpinDown;
   stateSequence[6]         = "Activate";
   //
   stateTimeoutValue[6]        = 0.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
   
   //--------------------------------------
   stateName[7]       = "DryFire";
   stateSound[7]      = ChaingunDryFireSound;
   stateTimeoutValue[7]        = 0.5;
   stateTransitionOnTimeout[7] = "NoAmmo";
};

function T1ChainGunImage::onFire(%data, %obj, %slot){
   %vector = %obj.getMuzzleVector(%slot);
   %x = (getRandom() - 0.5) * 2 * 3.1415926 * %data.projectileSpread;
   %y = (getRandom() - 0.5) * 2 * 3.1415926 * %data.projectileSpread;
   %z = (getRandom() - 0.5) * 2 * 3.1415926 * %data.projectileSpread;
   %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
   %vector = MatrixMulVector(%mat, %vector);

   %p = new (%data.projectileType)() {
      dataBlock        = "t1ChaingunBullet" @ getRandom(1,3);
      initialDirection = %vector;
      initialPosition  = %obj.getMuzzlePoint(%slot);
      sourceObject     = %obj;
      sourceSlot       = %slot;
   };
   MissionCleanup.add(%p);
   %obj.decInventory(%data.ammo,1);
   %obj.lastProjectile = %p;
   %client.projectile = %p; 
   return %p;
}


function T1ChainGunImage::onMount(%this,%obj,%slot){ 
   Parent::onMount(%this, %obj, %slot); 
}

function T1ChainGunImage::onUnmount(%this,%obj,%slot){
   Parent::onUnmount(%this, %obj, %slot);
}

function giveCG(){
   %player = LocalClientConnection.player; 
   %player.setInventory("T1ChainGun", 1, true);
   %player.setInventory("T1ChaingunAmmo", 200, true);
}

datablock AudioProfile(t1SensorHumSound)
{
   filename    = "t1sounds/pulse_power.wav";
   description = CloseLooping3d;
   preload = true;
};

datablock StaticShapeData(t1SensorMediumPulse) : StaticShapeDamageProfile
{
   className = Sensor;  
   catagory = "Sensors";
   shapeFile = "t1mSensor.dts"; 
   maxDamage = 1.2;
   destroyedLevel = 1.2;
   disabledLevel = 1.2;
   explosion      = ShapeExplosion;
   expDmgRadius = 7.0;
   expDamage = 0.4;
   expImpulse = 1500;

   dynamicType = $TypeMasks::SensorObjectType;
   isShielded = true;
   energyPerDamagePoint = 33;
   maxEnergy = 90;
   rechargeRate = 0.31;
   ambientThreadPowered = true;
   humSound = t1SensorHumSound;

   cmdCategory = "Support";
   cmdIcon = CMDSensorIcon;
   cmdMiniIconName = "commander/MiniIcons/com_sensor_grey";
   targetNameTag = 'Medium';
   targetTypeTag = 'Sensor';
   sensorData = SensorMedPulseObj;
   sensorRadius = SensorMedPulseObj.detectRadius;
   sensorColor = "255 194 9";

   debrisShapeName = "debris_generic.dts";
   debris = StaticShapeDebris;
};

datablock StaticShapeData(t1SensorLargePulse) : StaticShapeDamageProfile
{
   className = Sensor;  
   catagory = "Sensors";
   shapeFile = "t1LSensor.dts";
   maxDamage = 1.5;
   destroyedLevel = 1.5;
   disabledLevel = 0.85;
   explosion      = ShapeExplosion;
   expDmgRadius = 10.0;
   expDamage = 0.5;
   expImpulse = 2000.0;

   dynamicType = $TypeMasks::SensorObjectType;
   isShielded = true;
   energyPerDamagePoint = 33;
   maxEnergy = 110;
   rechargeRate = 0.31;
   ambientThreadPowered = true;
   humSound = t1SensorHumSound;

   cmdCategory = "Support";
   cmdIcon = CMDSensorIcon;
   cmdMiniIconName = "commander/MiniIcons/com_sensor_grey";
   targetNameTag = 'Large';
   targetTypeTag = 'Sensor';
   sensorData = SensorLgPulseObj;
   sensorRadius = SensorLgPulseObj.detectRadius;
   sensorColor = "255 194 9";

   debrisShapeName = "debris_generic.dts";
   debris = StaticShapeDebris;
};

datablock StaticShapeData(t1ammoPad)
{
   catagory = "Stations";
   shapeFile = "t1ammopad.dts";
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
   //humSound = StationInventoryHumSound;
   cmdCategory = "Support";
   cmdIcon = CMDStationIcon;
   cmdMiniIconName = "commander/MiniIcons/com_inventory_grey";
   targetNameTag = 'Inventory';
   targetTypeTag = 'Station';
   debrisShapeName = "debris_generic.dts";
   debris = StationDebris;

};

datablock StaticShapeData(t1VehPad)
{
   catagory = "Stations";
   shapeFile = "t1VehPad.dts";
   maxDamage = 10.00;
   destroyedLevel = 10.00;
   disabledLevel = 9.00;
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
   maxEnergy = 150;
   rechargeRate = 0.35;
   doesRepair = true;
   //humSound = StationInventoryHumSound;
   cmdCategory = "Support";
   cmdIcon = CMDVehicleStationIcon;
   cmdMiniIconName = "commander/MiniIcons/com_vehicle_pad_inventory";
   targetTypeTag = 'Vehicle Station';
   debrisShapeName = "debris_generic.dts";
   debris = StationDebris;

};

datablock StaticShapeData(t1VehStation)
{
   catagory = "Stations";
   shapeFile = "t1VehStation.dts";
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
   //humSound = StationInventoryHumSound;
   cmdCategory = "Support";
   cmdIcon = CMDVehicleStationIcon;
   cmdMiniIconName = "commander/MiniIcons/com_vehicle_pad_inventory";
   targetTypeTag = 'Vehicle Station';
   debrisShapeName = "debris_generic.dts";
   debris = StationDebris;

};

function t1VehStation::onAdd(%this, %obj)
{  
   Parent::onAdd(%this, %obj);
   %obj.init = 0;
   if(%obj.vPadObj $= ""){
      %obj.vPadObj = "replaceMe";
   }

   %trigger = new Trigger()
   { 
      dataBlock = t1StationTrigger;
      polyhedron = "-0.125 0.0 0.1 0.25 0.0 0.0 0.0 -0.8 0.0 0.0 0.0 1.0";
   };     

   MissionCleanup.add(%trigger);

   %trans = %obj.getTransform();
   %vSPos = getWords(%trans,0,2);
   %vRot =  getWords(%trans,3,5);
   %vAngle = getWord(%trans,6);
   %matrix = VectorOrthoBasis(%vRot @ " " @ %vAngle + 0.0);
   %yRot = getWords(%matrix, 3, 5);
   %pos = vectorAdd(%vSPos, vectorScale(%yRot, 1));

   %trigger.setTransform(%pos @ " " @ %vRot @ " " @ %vAngle);

   // associate the trigger with the station
   %trigger.station =%obj;
   %obj.trigger = %trigger;
}

function t1VehStation::onRemove(%this, %obj){
   parent::onRemove(%this, %obj);
   if(isObject(%obj.trigger)){
      %obj.trigger.delete();
   }
}

datablock StaticShapeData(t1CMDStation)
{
   catagory = "Stations";
   shapeFile = "t1CMDStation.dts";
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
   //humSound = StationInventoryHumSound;
   cmdCategory = "Support";
   cmdIcon = CMDStationIcon;
   cmdMiniIconName = "commander/MiniIcons/com_inventory_grey";
   targetNameTag = 'Command';
   targetTypeTag = 'Station';
   debrisShapeName = "debris_generic.dts";
   debris = StationDebris;

};

function t1CMDStation::onAdd(%this, %obj)
{
   Parent::onAdd(%this, %obj);
   %obj.init = 0;
   %trigger = new Trigger()
   { 
      dataBlock = t1StationTrigger;
      polyhedron = "-0.125 0.0 0.1 0.25 0.0 0.0 0.0 -0.8 0.0 0.0 0.0 1.0";
   };     

   MissionCleanup.add(%trigger);

   %trans = %obj.getTransform();
   %vSPos = getWords(%trans,0,2);
   %vRot =  getWords(%trans,3,5);
   %vAngle = getWord(%trans,6);
   %matrix = VectorOrthoBasis(%vRot @ " " @ %vAngle + 0.0);
   %yRot = getWords(%matrix, 3, 5);
   %pos = vectorAdd(%vSPos, vectorScale(%yRot, 1));

   %trigger.setTransform(%pos @ " " @ %vRot @ " " @ %vAngle);

   // associate the trigger with the station
   %trigger.station =%obj;
   %obj.trigger = %trigger;
}

function t1CMDStation::onRemove(%this, %obj){
   parent::onRemove(%this, %obj);
   if(isObject(%obj.trigger)){
      %obj.trigger.delete();
   }
}

datablock TriggerData(t1StationTrigger)
{
   tickPeriodMS = 30;
};

datablock AudioProfile(invyPowerActivate)
{
   filename    = "t1sounds/inv_power.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(invyPadActivate)
{
   filename    = "t1sounds/inv_activate.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(invyPadRun)
{
   filename    = "t1sounds/inv_use.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};

datablock AudioProfile(ammoPadActivate)
{
   filename    = "t1sounds/ammo_activate.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(ammoPadRun)
{
   filename    = "t1sounds/ammo_use.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};

datablock AudioProfile(cmdStationActivate)
{
   filename    = "t1sounds/command_activate.wav";
   description = AudioDefault3d;
   preload = true;
};


function t1StationTrigger::onEnterTrigger(%data, %trigger, %player){
   %station = %trigger.station;
   %targetname = %station.getDataBlock().getName(); 
   if(%trigger.powerTrig){
      if(%station.isEnabled() &&  %station.isPowered() && (!%player.client.lastInvySfx || (getSimTime() - %player.client.lastInvySfx) > 10000)){
         %player.client.play3D(invyPowerActivate, %trigger.getPosition());
         %player.client.lastInvySfx = getSimTime();
      }
      return;
   }

   if((%station.team != %player.client.team) && (%station.team != 0)){
      //%obj.station.playAudio(2, StationAccessDeniedSound);
      messageClient(%player.client, 'msgStationDenied', '\c2Access Denied -- Wrong team.~wt1sounds/Access_Denied.wav');
   }
   else if(%station.isDisabled()){
      messageClient(%player.client, 'msgStationDisabled', '\c2Station is disabled.');
   }
   else if(!%station.isPowered()){
      messageClient(%player.client, 'msgStationNoPower', '\c2Station is not powered.');
   }
   else if(%station.notDeployed){
      messageClient(%player.client, 'msgStationNotDeployed', '\c2Station is not deployed.');
   }
   else if(%station.isDestroyed){
      messageClient(%player.client, 'msgStationDestroyed', '\c2Station is destroyed.');
   }
   else if(isObject(%station) && %station.isEnabled() &&  %station.isPowered()){
      messageClient(%player.client, 'CloseHud', "", 'inventoryScreen');
      commandToClient(%player.client, 'TogglePlayHuds', true);
      if(%targetname $= "t1ammoPad"){
         %station.playAudio(2, ammoPadActivate);
         %station.schRunHum = %station.schedule(1300,"playAudio", 3, ammoPadRun);
         t1buyDeployableFavorites(%player.client);
         %oldRate = %player.getRepairRate();
         %player.setRepairRate(%oldRate + 0.00625);
         if(%station.init){
               %station.setThreadDir(2, true);
         }
         else{
               %station.playThread(2, "activate"); 
               %station.init = 1;
         } 
      }
      else if(%targetname $= "t1InvyPad"){
         %station.playAudio(2, invyPadActivate);
         %station.playAudio(3, invyPadRun);
         t1buyFavorites(%player.client);
         %plrRot = getWords(%player.getTransform(), 3, 6);
         %fvec = %station.getForwardVector();
         %sPos = vectorAdd(%station.getPosition(),"0 0 0.25");
         %player.setTransform(vectorAdd(%sPos,vectorScale(%fvec,1.5)) SPC %plrRot);
         %oldRate = %player.getRepairRate();
         %player.setRepairRate(%oldRate + 0.00625);

         if(%station.init){
               %station.setThreadDir(2, true);
         }
         else{
               %station.playThread(2, "activate");
               %station.init = 1;
         } 

      }
      else if(%targetname $= "t1CMDStation"){
         %station.playAudio(2, cmdStationActivate);
         commandToClient(%player.client,'OpenCommanderMap',1);
         if(%station.init){
               %station.setThreadDir(2, true);
         }
         else{
               %station.playThread(2, "activate"); 
               %station.init = 1;
         } 
      }
      else if(%targetname $= "t1VehStation"){
         commandToClient(%player.client,'PickVehMenu', 1, "buyT1Veh", "Buy Vehicle","","Scout", "Light Transport", "Heavy Transport");
         %player.atVPad =  %station;
         %station.playAudio(2, invyPadActivate);
         %station.playAudio(3, invyPadRun);
         if(%station.init){
               %station.setThreadDir(2, true);
         }
         else{
               %station.playThread(2, "activate"); 
               %station.init = 1;
         } 
      }
      else if(%targetname $= "T1InvyDeployableObj"){
         %station.playAudio(2, ammoPadRun);
         t1buyDeployableFavorites(%player.client);
         %oldRate = %player.getRepairRate();
         %player.setRepairRate(%oldRate + 0.00625);
         %station.playThread(2, "activate"); 
      }
      else if(%targetname $= "T1AmmoDeployableObj"){
         %station.playAudio(2, ammoPadRun);
         T1getAmmoStationLovin(%player.client);
         %oldRate = %player.getRepairRate();
         %player.setRepairRate(%oldRate + 0.00625);
         %station.playThread(2, "activate"); 
      }
   }
   %trigger.lastObj = %player;
}

function t1StationTrigger::onleaveTrigger(%data, %trigger, %player){
   %station = %trigger.station;
   %targetname = %station.getDataBlock().getName(); 
   if(%player.team == %station.team && !%trigger.powerTrig){
      if(isObject(%station)){
         if(%targetname $= "t1ammoPad"){
            %station.setThreadDir(2, false);
            if(!%station.isDestroyed && %station.isPowered() && !%station.isDisabled()){
               %station.playAudio(2, ammoPadActivate);
            }
            %player.setRepairRate(0);
            %station.stopAudio(3);
            cancel(%station.schRunHum);
         }
         else if(%targetname $= "t1InvyPad"){
            %station.setThreadDir(2, false);
            if(!%station.isDestroyed && %station.isPowered() && !%station.isDisabled()){
               %station.playAudio(2, invyPadActivate);   
            }
            %player.setRepairRate(0);
            %station.stopAudio(3);
         }
         else if(%targetname $= "t1CMDStation"){
            %station.setThreadDir(2, false);
            %station.playAudio(2, cmdStationActivate);
         }
         else if(%targetname $= "t1VehStation"){
            %station.setThreadDir(2, false);
            if(!%station.isDestroyed && %station.isPowered() && !%station.isDisabled()){
               %station.playAudio(2, invyPadActivate);   
            }
            %station.stopAudio(3);
            commandToClient(%player.client,'PickVehMenu', 0);
            %player.atVPad = "";
         }
         else if(%targetname $= "T1InvyDeployableObj"){
             %station.stopThread(2);
            %player.setRepairRate(0);
            %station.stopAudio(2);
            cancel(%station.schRunHum);
         }
         else if(%targetname $= "T1AmmoDeployableObj"){
             %station.stopThread(2);
            %player.setRepairRate(0);
            %station.stopAudio(2);
            cancel(%station.schRunHum);
         }

         if(%player.getMountedImage($WeaponSlot) == 0)
         {
            if(%player.inv[%player.lastWeapon])
               %player.use(%player.lastWeapon);
            else 
               %player.selectWeaponSlot( 0 );
         }
      }
   }
   %trigger.lastObj = "";
}

function t1StationTrigger::onTickTrigger(%data, %trig){
	return;
}

function t1ammoPad::onAdd(%this, %obj)
{
   Parent::onAdd(%this, %obj);
   %obj.init = 0;
   %trigger = new Trigger()
   { 
      dataBlock = t1StationTrigger;
      polyhedron = "-0.125 0.0 0.1 0.25 0.0 0.0 0.0 -0.8 0.0 0.0 0.0 1.0";
   };     

   MissionCleanup.add(%trigger);

   %trans = %obj.getTransform();
   %vSPos = getWords(%trans,0,2);
   %vRot =  getWords(%trans,3,5);
   %vAngle = getWord(%trans,6);
   %matrix = VectorOrthoBasis(%vRot @ " " @ %vAngle + 0.0);
   %yRot = getWords(%matrix, 3, 5);
   %pos = vectorAdd(%vSPos, vectorScale(%yRot, 1));

   %trigger.setTransform(%pos @ " " @ %vRot @ " " @ %vAngle);

   // associate the trigger with the station
   %trigger.station =%obj;
   %obj.trigger = %trigger;
}

function t1ammoPad::onRemove(%this, %obj){
   parent::onRemove(%this, %obj);
   if(isObject(%obj.trigger)){
      %obj.trigger.delete();
   }
}

// function t1ammoPadCol::onDestroyed(%data, %obj, %prevState){
//    parent::onDestroyed(%data, %obj, %prevState);
//    if(isObject(%obj.sobj)){
//       %obj.sobj.setDamageState("Destroyed");
//       %obj.sobj.stopThread(1);
//    }
// }
// function t1ammoPadCol::onEnabled(%data, %obj, %prevState){
//    parent::onEnabled(%data, %obj, %prevState);
//    if(isObject(%obj.sobj)){
//       %obj.sobj.setDamageState("Enabled");
//        %obj.sobj.playThread(1,power);
//    }
// }

// function t1ammoPadCol::onGainPowerEnabled(%data, %obj){
//    Parent::onGainPowerEnabled(%data, %obj);
//    if(isObject(%obj.sobj)){
//        %obj.sobj.setDamageState("Enabled");
//        %obj.sobj.playThread(1,power);
//    }

// }

// function t1ammoPadCol::onLosePowerDisabled(%data, %obj){
//    Parent::onLosePowerDisabled(%data, %obj);
//    if(isObject(%obj.sobj)){
//       %obj.sobj.stopThread(1);
//    }
// }


datablock StaticShapeData(t1InvyPad)
{
   catagory = "Stations";
   shapeFile = "t1inventorystation.dts";
   //shapeFile = "t1ammopad.dts";
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
   //humSound = StationInventoryHumSound;
   cmdCategory = "Support";
   cmdIcon = CMDStationIcon;
   cmdMiniIconName = "commander/MiniIcons/com_inventory_grey";
   targetNameTag = 'Inventory';
   targetTypeTag = 'Station';
   debrisShapeName = "debris_generic.dts";
   debris = StationDebris;

};


function t1InvyPad::onAdd(%this, %obj)
{
   Parent::onAdd(%this, %obj);
   %obj.init = 0;
   %trigger = new Trigger()
   { 
      dataBlock = t1StationTrigger;
      polyhedron = "-0.5 0.5 -0.5 1.0 0.0 0.0 -0.0 -1.0 -0.0 -0.0 -0.0 1.0";
      powerTrig = 1;
      scale = "12 12 8";
   };     
   MissionCleanup.add(%trigger);
   %trigger.setTransform(%obj.getTransform());
   %trigger.station = %obj;
   %obj.pwrTrigger = %trigger;

   %trigger = new Trigger()
   { 
      dataBlock = t1StationTrigger;
      polyhedron = "-1.0 0.0 0.1 2.0 0.0 0.0 0.0 -1 0.0 0.0 0.0 2.0";//
   };     
   MissionCleanup.add(%trigger);
   %obj.trigger = %trigger;

   %trans = %obj.getTransform();
   %vSPos = getWords(%trans,0,2);
   %vRot =  getWords(%trans,3,5);
   %vAngle = getWord(%trans,6);
   %matrix = VectorOrthoBasis(%vRot @ " " @ %vAngle + 0.0);
   %yRot = getWords(%matrix, 3, 5);
   %pos = vectorAdd(%vSPos, vectorScale(%yRot, 1.25));
   %trigger.setTransform(%pos @ " " @ %vRot @ " " @ %vAngle);

   // associate the trigger with the station
   %trigger.station = %obj;
}

function t1InvyPad::onRemove(%this, %obj){
   parent::onRemove(%this, %obj);
   if(isObject(%obj.trigger)){
      %obj.trigger.delete();
   }
   if(isObject(%obj.pwrTrigger)){
      %obj.pwrTrigger.delete();
   }
}

// function t1InvyPad::onDestroyed(%data, %obj, %prevState){
//    parent::onDestroyed(%data, %obj, %prevState);
//    if(isObject(%obj.sobj)){
//       %obj.sobj.setDamageState("Destroyed");
//       %obj.sobj.stopThread(1);
//    }
// }
// function t1InvyPad::onEnabled(%data, %obj, %prevState){
//    parent::onEnabled(%data, %obj, %prevState);
//    if(isObject(%obj.sobj)){
//       %obj.sobj.setDamageState("Enabled");
//        %obj.sobj.playThread(1,power);
//    }
// }

// function t1InvyPad::onGainPowerEnabled(%data, %obj){
//    Parent::onGainPowerEnabled(%data, %obj);
//    if(isObject(%obj.sobj)){
//        %obj.sobj.setDamageState("Enabled");
//        %obj.sobj.playThread(1,power);
//    }

// }

// function t1InvyPad::onLosePowerDisabled(%data, %obj){
//    Parent::onLosePowerDisabled(%data, %obj);
//    if(isObject(%obj.sobj)){
//       %obj.sobj.stopThread(1);
//    }
// }


datablock AudioProfile(T1PlasmaBarrelExpSound)
{
   filename    = "t1sounds/turretexp.wav";
   description = "AudioExplosion3d";
   preload = true;
};
datablock AudioProfile(T1TurretFireSound)
{
   filename    = "t1sounds/turretfire4.wav";
   description = "AudioExplosion3d";
   preload = true;
};
datablock AudioProfile(t1TurretReady)
{
   filename    = "t1sounds/turreton4.wav";
   description = "AudioExplosion3d";
   preload = true;
};
datablock AudioProfile(t1TurretPark)
{
   filename    = "t1sounds/turretoff4.wav";
   description = "AudioExplosion3d";
   preload = true;
};

datablock ExplosionData(T1PlasmaBarrelBoltExplosion)
{
   soundProfile   = T1PlasmaBarrelExpSound;
   particleEmitter = PlasmaBarrelExplosionEmitter;
   particleDensity = 250;
   particleRadius = 1.25;
   faceViewer = true;

   emitter[0] = PlasmaBarrelCrescentEmitter;

   subExplosion[0] = PlasmaBarrelSubExplosion1;
   subExplosion[1] = PlasmaBarrelSubExplosion2;
   subExplosion[2] = PlasmaBarrelSubExplosion3;

   shakeCamera = true;
   camShakeFreq = "10.0 9.0 9.0";
   camShakeAmp = "10.0 10.0 10.0";
   camShakeDuration = 0.5;
   camShakeRadius = 15.0;
};

datablock LinearFlareProjectileData(T1PlasmaBarrelBolt)
{
   doDynamicClientHits = true;

   directDamage        = 0;
   directDamageType    = $DamageType::PlasmaTurret;
   hasDamageRadius     = true;
   // z0dd - ZOD, 5/07/04. Lower damage if gameplay changes in affect
   indirectDamage      = $Host::ClassicLoadPlasmaTurretChanges ? 1.0 : 1.2;
   damageRadius        = 10.0;
   kickBackStrength    = 500;
   radiusDamageType    = $DamageType::PlasmaTurret;
   explosion           = T1PlasmaBarrelBoltExplosion;
   splash              = PlasmaSplash;

   // z0dd - ZOD, 5/07/04. Lower velocity if gameplay changes in affect
   dryVelocity       = $Host::ClassicLoadPlasmaTurretChanges ? 65 : 75;
   wetVelocity       = -1;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 4000;
   lifetimeMS        = 4000; // z0dd - ZOD, 4/25/02. Was 6000
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 100;

   scale             = "1.5 1.5 1.5";
   numFlares         = 30;
   flareColor        = "0.1 0.3 1.0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";
};


datablock TurretImageData(hellFireTurretBarrel)
{
   shapeFile = "hellFireGun.dts";

   projectile = T1PlasmaBarrelBolt;
   projectileType = LinearFlareProjectile;
   usesEnergy = true;
   fireEnergy = 10;
   minEnergy = 10;
   emap = true;

   // Turret parameters
   activationMS      = 1000; // z0dd - ZOD, 3/27/02. Was 1000. Amount of time it takes turret to unfold
   deactivateDelayMS = 1500;
   thinkTimeMS       = 140; // z0dd - ZOD, 3/27/02. Was 200. Amount of time before turret starts to unfold (activate)
   degPerSecTheta    = 300;
   degPerSecPhi      = 500;
   attackRadius      = 150; // z0dd - ZOD, 3/27/02. Was 120

   // State transitions
   stateName[0]                  = "Activate";
   stateTransitionOnNotLoaded[0] = "Dead";
   stateTransitionOnLoaded[0]    = "ActivateReady";

   stateName[1]                  = "ActivateReady";
   stateSequence[1]              = "Activate";
   stateSound[1]                 = t1TurretReady;
   stateTimeoutValue[1]          = 1;
   stateTransitionOnTimeout[1]   = "Ready";
   stateTransitionOnNotLoaded[1] = "Deactivate";
   stateTransitionOnNoAmmo[1]    = "NoAmmo";

   stateName[2]                    = "Ready";
   stateTransitionOnNotLoaded[2]   = "Deactivate";
   stateTransitionOnTriggerDown[2] = "Fire";
   stateTransitionOnNoAmmo[2]      = "NoAmmo";

   stateName[3]                = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3]        = 0.3;
   stateFire[3]                = true;
   stateRecoil[3]              = LightRecoil;
   stateAllowImageChange[3]    = false;
   stateSequence[3]            = "Fire";
   stateSound[3]               = T1TurretFireSound;
   stateScript[3]              = "onFire";

   stateName[4]                  = "Reload";
   stateTimeoutValue[4]          = 0.80;
   stateAllowImageChange[4]      = false;
   stateSequence[4]              = "Reload";
   stateTransitionOnTimeout[4]   = "Ready";
   stateTransitionOnNotLoaded[4] = "Deactivate";
   stateTransitionOnNoAmmo[4]    = "NoAmmo";

   stateName[5]                = "Deactivate";
   stateSound[5]               = t1TurretPark;
   stateSequence[5]            = "Activate";
   stateDirection[5]           = false;
   stateTimeoutValue[5]        = 1;
   stateTransitionOnLoaded[5]  = "ActivateReady";
   stateTransitionOnTimeout[5] = "Dead";

   stateName[6]               = "Dead";
   stateTransitionOnLoaded[6] = "ActivateReady";

   stateName[7]             = "NoAmmo";
   stateTransitionOnAmmo[7] = "Reload";
   stateSequence[7]         = "NoAmmo";
};


datablock TurretData(hellFireTurret) : TurretDamageProfile
{
   //className = Turret;
   catagory = "Turrets";
   shapeFile = "hellFireTurret.dts";

   barrel = hellFireTurretBarrel;
   
   preload        = true;

   mass           = 1.0;  // Not really relevant

   maxDamage      = 2.25;
   destroyedLevel = 2.25;
   disabledLevel  = 1.35;
   explosion      = TurretExplosion;
	expDmgRadius = 15.0;
	expDamage = 0.66;
	expImpulse = 2000.0;
   repairRate     = 0;
   emap = true;
   
   thetaMin = 15;
   thetaMax = 140;

   isShielded           = true;
   energyPerDamagePoint = 50;
   maxEnergy = 150;
   rechargeRate = 0.31;
   //humSound = BaseTurretHumSound; // Was SensorHumSound
   pausePowerThread = true;

   canControl = true;
   cmdCategory = "Tactical";
   cmdIcon = CMDTurretIcon;
   cmdMiniIconName = "commander/MiniIcons/com_turretbase_grey";
   targetNameTag = 'Base';
   targetTypeTag = 'Turret';
   sensorData = TurretBaseSensorObj;
   sensorRadius = TurretBaseSensorObj.detectRadius;
   sensorColor = "0 212 45";

   firstPersonOnly = true;

   debrisShapeName = "debris_generic.dts";
   debris = TurretDebris;


};

function hellFireTurret::onAdd(%data, %obj){
   Parent::onAdd(%data, %obj);
   %obj.mountImage(%data.barrel, 0, true);
}

datablock AudioProfile(T1BlasterFireSound)
{
   filename    = "t1sounds/rifle1.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock ExplosionData(T1SentryTurretExplosion)
{
   explosionShape = "energy_explosion.dts";
   soundProfile   = T1BlasterExpSound;

   particleEmitter = SentryTurretExplosionEmitter;
   particleDensity = 120;
   particleRadius = 0.15;

   faceViewer           = false;
};

datablock LinearFlareProjectileData(T1SentryTurretEnergyBolt)
{
   directDamage        = 0.1;
   directDamageType    = $DamageType::SentryTurret;

   explosion           = "T1SentryTurretExplosion";
   kickBackStrength  = 0.0;

   dryVelocity       = 200.0;
   wetVelocity       = 150.0;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 2000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   numFlares         = 10;
   size              = 0.35;
   flareColor        = "0.35 0.35 1";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   //sound = SentryTurretProjectileSound;

   hasLight    = true;
   lightRadius = 1.5;
   lightColor  = "0.35 0.35 1";
};

datablock TurretImageData(T1SentryTurretBarrel)
{
   shapeFile = "turret_muzzlepoint.dts";

   projectile = T1SentryTurretEnergyBolt;
   projectileType = LinearFlareProjectile;
   usesEnergy = true;
   fireEnergy = 6.00;
   minEnergy = 6.00;
   emap = true;

   // Turret parameters
   activationMS      = 300;
   deactivateDelayMS = 500;
   thinkTimeMS       = 200;
   degPerSecTheta    = 520;
   degPerSecPhi      = 960;
   attackRadius      = 60;


      // State transitions
   stateName[0]                  = "Activate";
   stateTransitionOnNotLoaded[0] = "Dead";
   stateTransitionOnLoaded[0]    = "ActivateReady";

   stateName[1]                  = "ActivateReady";
   stateSequence[1]              = "Activate";
   stateSound[1]                 = t1TurretReady;
   stateTimeoutValue[1]          = 0.1;
   stateTransitionOnTimeout[1]   = "Ready";
   stateTransitionOnNotLoaded[1] = "Deactivate";
   stateTransitionOnNoAmmo[1]    = "NoAmmo";

   stateName[2]                    = "Ready";
   stateTransitionOnNotLoaded[2]   = "Deactivate";
   stateTransitionOnTriggerDown[2] = "Fire";
   stateTransitionOnNoAmmo[2]      = "NoAmmo";

   stateName[3]                = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3]        = 0.13;
   stateFire[3]                = true;
   stateRecoil[3]              = LightRecoil;
   stateAllowImageChange[3]    = false;
   stateSequence[3]            = "Fire";
   stateSound[3]               = T1BlasterFireSound;
   stateScript[3]              = "onFire";

   stateName[4]                  = "Reload";
   stateTimeoutValue[4]          = 0.40;
   stateAllowImageChange[4]      = false;
   stateSequence[4]              = "Reload";
   stateTransitionOnTimeout[4]   = "Ready";
   stateTransitionOnNotLoaded[4] = "Deactivate";
   stateTransitionOnNoAmmo[4]    = "NoAmmo";

   stateName[5]                = "Deactivate";
   stateSound[5]               = t1TurretPark;
   stateSequence[5]            = "Activate";
   stateDirection[5]           = false;
   stateTimeoutValue[5]        = 0.1;
   stateTransitionOnLoaded[5]  = "ActivateReady";
   stateTransitionOnTimeout[5] = "Dead";

   stateName[6]               = "Dead";
   stateTransitionOnLoaded[6] = "ActivateReady";

   stateName[7]             = "NoAmmo";
   stateTransitionOnAmmo[7] = "Reload";
   stateSequence[7]         = "NoAmmo";
};

datablock TurretData(T1SentryTurret) : TurretDamageProfile
{
   //className = Turret;
   catagory = "Turrets";
   shapeFile = "t1Sentry.dts";

   //Uses the same stats as an outdoor deployable turret (balancing info for Dave)
   mass = 5.0;

   barrel = T1SentryTurretBarrel;

   maxDamage = 1.2;
   destroyedLevel = 1.2;
   disabledLevel = 0.84;
   explosion      = ShapeExplosion;
   expDmgRadius = 5.0;
   expDamage = 0.4;
   expImpulse = 1000.0; 
   repairRate = 0;

   thetaMin = 0;
   thetaMax = 180;
   emap = true;
   

   isShielded           = true;
   energyPerDamagePoint = 100;
   maxEnergy = 150;
   rechargeRate = 0.40;

   canControl = true;
   cmdCategory = "Tactical";
   cmdIcon = CMDTurretIcon;
   cmdMiniIconName = "commander/MiniIcons/com_turret_grey";
   targetNameTag = 'Sentry';
   targetTypeTag = 'Turret';
   sensorData = SentryMotionSensor;
   sensorRadius = SentryMotionSensor.detectRadius;
   sensorColor = "9 136 255";
   firstPersonOnly = false;
};

function T1SentryTurret::onAdd(%data, %obj){
   Parent::onAdd(%data, %obj);
   %obj.mountImage(%data.barrel, 0, true);
}

datablock AudioProfile(t1GeneratorHumSound)
{
   filename    = "t1sounds/generator.wav";
   description = ClosestLooping3d;
};

datablock StaticShapeData(T1GeneratorLarge) : StaticShapeDamageProfile 
{   
   className      = Generator;
   catagory       = "Generators";
   shapeFile      = "t1PowerGen.dts";
   explosion      = ShapeExplosion;
   maxDamage      = 1.5;
   destroyedLevel = 1.5;
   disabledLevel  = 0.85;
   expDmgRadius = 10.0;
   expDamage = 0.5;
   expImpulse = 1500.0;
   noIndividualDamage = true; //flag to make these invulnerable for certain mission types

   dynamicType = $TypeMasks::GeneratorObjectType;
   isShielded = true;
   energyPerDamagePoint = 30;
   maxEnergy = 70; // z0dd - ZOD, 09/20/02. Was 50
   rechargeRate = 0.15; // z0dd - ZOD, 09/20/02. Was 0.05
   humSound = t1GeneratorHumSound;
   
   cmdCategory = "Support";
   cmdIcon = "CMDGeneratorIcon";
   cmdMiniIconName = "commander/MiniIcons/com_generator";   
   targetTypeTag = 'Generator';

   debrisShapeName = "debris_generic.dts";
   debris = StaticShapeDebris;
};

datablock StaticShapeData(T1GeneratorMed) : StaticShapeDamageProfile 
{   
   className      = Generator;
   catagory       = "Generators";
   shapeFile      = "t1pGen.dts";
   explosion      = ShapeExplosion;
   maxDamage      = 1.5;
   destroyedLevel = 1.5;
   disabledLevel  = 0.85;
   expDmgRadius = 10.0;
   expDamage = 0.5;
   expImpulse = 1500.0;
   noIndividualDamage = true; //flag to make these invulnerable for certain mission types

   dynamicType = $TypeMasks::GeneratorObjectType;
   isShielded = true;
   energyPerDamagePoint = 30;
   maxEnergy = 70; // z0dd - ZOD, 09/20/02. Was 50
   rechargeRate = 0.15; // z0dd - ZOD, 09/20/02. Was 0.05
   humSound = t1GeneratorHumSound;
   
   cmdCategory = "Support";
   cmdIcon = "CMDGeneratorIcon";
   cmdMiniIconName = "commander/MiniIcons/com_generator";   
   targetTypeTag = 'Generator';

   debrisShapeName = "debris_generic.dts";
   debris = StaticShapeDebris;
};


datablock StaticShapeData(t1SolarPanel) : StaticShapeDamageProfile
{
   className = Generator;
   catagory = "Generators";
   shapeFile = "t1Solar.dts";
   explosion = ShapeExplosion;
   maxDamage = 1.00;
   destroyedLevel = 1.00;
   disabledLevel = 0.55;
   expDmgRadius = 5.0;
   expDamage = 0.3;
   expImpulse = 1000.0;
   noIndividualDamage = true; //flag to make these invulnerable for certain mission types
   emap = true;

   isShielded = true;
   energyPerDamagePoint = 30;
   rechargeRate = 0.1; // z0dd - ZOD, 09/20/02. Was 0.05

   dynamicType = $TypeMasks::GeneratorObjectType;
   maxEnergy = 30;
   //humSound = GeneratorHumSound;

   cmdCategory = "Support";
   cmdIcon = CMDSolarGeneratorIcon;
   cmdMiniIconName = "commander/MiniIcons/com_solargen_grey";
   targetTypeTag = 'Solar Panel';

   debrisShapeName = "debris_generic.dts";
   debris = StaticShapeDebris;
};

datablock SeekerProjectileData(t1TurretMissile)
{
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 1.0;
   damageRadius        = 4.0;
   radiusDamageType    = $DamageType::MissileTurret;
   kickBackStrength    = 2500;

   flareDistance = 200;
   flareAngle    = 30;
   minSeekHeat   = 0.05;

   explosion           = "MissileExplosion";
   velInheritFactor    = 0.2;

   splash              = MissileSplash;
   baseEmitter         = MissileSmokeEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;

   lifetimeMS          = 20000;
   muzzleVelocity      = 90.0; // z0dd - ZOD, 3/27/02. Was 80. Velocity of projectile
   turningSpeed        = 90.0;
   
   proximityRadius     = 4;

   terrainAvoidanceSpeed = 180;
   terrainScanAhead      = 25;
   terrainHeightFail     = 12;
   terrainAvoidanceRadius = 100;

   useFlechette = true;
   flechetteDelayMs = 550;
   casingDeb = FlechetteDebris;
};
datablock AudioProfile(t1MissilePark)
{
   filename    = "t1sounds/turretoff1.wav";
   description = AudioDefault3d;
   preload = true;
};
datablock AudioProfile(t1MissileAct)
{
   filename    = "t1sounds/turreton1.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(t1MissileFire)
{
   filename    = "t1sounds/turretfire1.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock TurretImageData(T1MissileBarrel)
{
   shapeFile = "turret_muzzlepoint.dts";
   item      = MissileBarrelPack; // z0dd - ZOD, 4/25/02. Was wrong: MissileBarrelLargePack

   projectile = t1TurretMissile;
   projectileType = SeekerProjectile;

   usesEnergy = true;
   fireEnergy = 60.0;
   minEnergy = 60.0;

   isSeeker     = true;
   seekRadius   = 200;
   maxSeekAngle = 30;
   seekTime     = 1.0;
   minSeekHeat  = 0.05;
   emap = true;
   minTargetingDistance = 20;

   // Turret parameters
   activationMS      = 2000; // z0dd - ZOD, 3/27/02. Was 250. Amount of time it takes turret to unfold
   deactivateDelayMS = 1500;
   thinkTimeMS       = 256; // z0dd - ZOD, 3/27/02. Was 200. Amount of time before turret starts to unfold (activate)
   degPerSecTheta    = 580;
   degPerSecPhi      = 1080;
   attackRadius      = 150;

   // State transitions
   stateName[0]                  = "Activate";
   stateTransitionOnNotLoaded[0] = "Dead";
   stateTransitionOnLoaded[0]    = "ActivateReady";

   stateName[1]                  = "ActivateReady";
   stateSequence[1]              = "Activate";
   stateSound[1]                 = t1MissileAct; // z0dd - ZOD, 3/27/02. Was MBLSwitchSound, changed for sound fix.
   stateTimeoutValue[1]          = 2.2;
   stateTransitionOnTimeout[1]   = "Ready";
   stateTransitionOnNotLoaded[1] = "Deactivate";
   stateTransitionOnNoAmmo[1]    = "NoAmmo";

   stateName[2]                    = "Ready";
   stateTransitionOnNotLoaded[2]   = "Deactivate";
   stateTransitionOnTriggerDown[2] = "Fire";
   stateTransitionOnNoAmmo[2]      = "NoAmmo";

   stateName[3]                = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3]        = 0.3;
   stateFire[3]                = true;
   stateRecoil[3]              = LightRecoil;
   stateAllowImageChange[3]    = false;
   stateSequence[3]            = "Fire";
   stateSound[3]               = t1MissileFire; // z0dd - ZOD, 3/27/02. Was MBLFireSound, changed for sound fix.
   stateScript[3]              = "onFire";

   stateName[4]                  = "Reload";
   stateTimeoutValue[4]          = 3.5;
   stateAllowImageChange[4]      = false;
   stateSequence[4]              = "Reload";
   stateTransitionOnTimeout[4]   = "Ready";
   stateTransitionOnNotLoaded[4] = "Deactivate";
   stateTransitionOnNoAmmo[4]    = "NoAmmo";

   stateName[5]                = "Deactivate";
   stateSound[5]               = t1MissilePark;
   stateSequence[5]            = "Activate";
   stateDirection[5]           = false;
   stateTimeoutValue[5]        = 2.2;
   stateTransitionOnLoaded[5]  = "ActivateReady";
   stateTransitionOnTimeout[5] = "Dead";

   stateName[6]               = "Dead";
   stateTransitionOnLoaded[6] = "ActivateReady";

   stateName[7]             = "NoAmmo";
   stateTransitionOnAmmo[7] = "Reload";
   stateSequence[7]         = "NoAmmo";
};

datablock TurretData(t1MissileTurret) : TurretDamageProfile
{
   //className = Turret;
   catagory = "Turrets";
   shapeFile = "t1MisTurret.dts";

   barrel = T1MissileBarrel;
   
   preload        = true;

   mass           = 1.0;  // Not really relevant

   maxDamage      = 2.25;
   destroyedLevel = 2.25;
   disabledLevel  = 1.35;
   explosion      = TurretExplosion;
	expDmgRadius = 15.0;
	expDamage = 0.66;
	expImpulse = 2000.0;
   repairRate     = 0;
   emap = true;
   
   thetaMin = 15;
   thetaMax = 140;

   isShielded           = true;
   energyPerDamagePoint = 50;
   maxEnergy = 150;
   rechargeRate = 0.31;
   //humSound = BaseTurretHumSound; // Was SensorHumSound
   pausePowerThread = true;

   canControl = true;
   cmdCategory = "Tactical";
   cmdIcon = CMDTurretIcon;
   cmdMiniIconName = "commander/MiniIcons/com_turretbase_grey";
   targetNameTag = 'Base';
   targetTypeTag = 'Turret';
   sensorData = TurretBaseSensorObj;
   sensorRadius = TurretBaseSensorObj.detectRadius;
   sensorColor = "0 212 45";

   firstPersonOnly = true;

   debrisShapeName = "debris_generic.dts";
   debris = TurretDebris;


};

function T1MissileBarrel::onFire(%data,%obj,%slot)
{
   %p = Parent::onFire(%data,%obj,%slot);
   //--------------------------------------------------------
   // z0dd - ZOD, 9/3/02. Anti rapid fire mortar/missile fix.
   if(!%p)
   {
      return;	
   }
   //--------------------------------------------------------
   MissileSet.add(%p); // z0dd - ZOD, 8/10/03. Bots need this.
   if (%obj.getControllingClient())
   {
      // a player is controlling the turret
      %target = %obj.getLockedTarget();
   }
   else
   {
      // The ai is controlling the turret
      %target = %obj.getTargetObject();
   }
   if(%target)
      %p.setObjectTarget(%target);
   else if(%obj.isLocked())
      %p.setPositionTarget(%obj.getLockedPosition());
   else
      %p.setNoTarget(); // set as unguided. Only happens when itchy trigger can't wait for lock tone.
}


function t1MissileTurret::onAdd(%data, %obj){
   Parent::onAdd(%data, %obj);
   %obj.mountImage(%data.barrel, 0, true);
}


datablock TurretImageData(T1ELFBarrelLarge)
{
   shapeFile = "turret_muzzlepoint.dts";
   item      = ELFBarrelPack; // z0dd - ZOD, 4/25/02. Was wrong: ELFBarrelLargePack

   projectile = ELFTurretBolt;
   projectileType = ELFProjectile;
   deleteLastProjectile = true;
   usesEnergy = true;
   fireEnergy = 0.0;
   minEnergy = 0.0;

   // Turret parameters
   activationMS      = 1500; // z0dd - ZOD, 3/27/02. Was 500. Amount of time it takes turret to unfold
   deactivateDelayMS = 1500;
   thinkTimeMS       = 70; // z0dd - ZOD, 3/27/02. Was 100. Amount of time before turret starts to unfold (activate)
   degPerSecTheta    = 580;
   degPerSecPhi      = 960;
   attackRadius      = 75;

   yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances
   
   // State transiltions
   stateName[0]                        = "Activate";
   stateTransitionOnNotLoaded[0]       = "Dead";
   stateTransitionOnLoaded[0]          = "ActivateReady";

   stateName[1]                        = "ActivateReady";
   stateSequence[1]                    = "Activate";
   stateSound[1]                       = t1TurretReady;
   stateTimeoutValue[1]                = 1;
   stateTransitionOnTimeout[1]         = "Ready";
   stateTransitionOnNotLoaded[1]       = "Deactivate";
   stateTransitionOnNoAmmo[1]          = "NoAmmo";

   stateName[2]                        = "Ready";
   stateTransitionOnNotLoaded[2]       = "Deactivate";
   stateTransitionOnTriggerDown[2]     = "Fire";
   stateTransitionOnNoAmmo[2]          = "NoAmmo";

   stateName[3]                        = "Fire";
   stateFire[3]                        = true;
   stateRecoil[3]                      = LightRecoil;
   stateAllowImageChange[3]            = false;
   stateSequence[3]                    = "Fire";
   stateSound[3]                       = EBLFireSound;
   stateScript[3]                      = "onFire";
   stateTransitionOnTriggerUp[3]       = "Deconstruction";
   stateTransitionOnNoAmmo[3]          = "Deconstruction";

   stateName[4]                        = "Reload";
   stateTimeoutValue[4]                = 0.01;
   stateAllowImageChange[4]            = false;
   stateSequence[4]                    = "Reload";
   stateTransitionOnTimeout[4]         = "Ready";
   stateTransitionOnNotLoaded[4]       = "Deactivate";
   stateTransitionOnNoAmmo[4]          = "NoAmmo";

   stateName[5]                        = "Deactivate";
   stateSequence[5]                    = "Activate";
   stateDirection[5]                   = false;
   stateTimeoutValue[5]                = 1;
   stateSound[5]                       = t1TurretPark;
   stateTransitionOnLoaded[5]          = "ActivateReady";
   stateTransitionOnTimeout[5]         = "Dead";

   stateName[6]                        = "Dead";
   stateTransitionOnLoaded[6]          = "ActivateReady";

   stateName[7]                        = "NoAmmo";
   stateTransitionOnAmmo[7]            = "Reload";
   stateSequence[7]                    = "NoAmmo";
   
   stateName[8]                       = "Deconstruction";
   stateScript[8]                     = "deconstruct";
   stateTransitionOnNoAmmo[8]         = "NoAmmo";
   stateTransitionOnTimeout[8]        = "Reload";
   stateTimeoutValue[8]               = 0.1;
};

datablock TurretData(t1ElfTurret) : TurretDamageProfile
{
   //className = Turret;
   catagory = "Turrets";
   shapeFile = "t1elfTurret.dts";

   barrel = T1ELFBarrelLarge;
   
   preload        = true;

   mass           = 1.0;  // Not really relevant

   maxDamage      = 2.25;
   destroyedLevel = 2.25;
   disabledLevel  = 1.35;
   explosion      = TurretExplosion;
	expDmgRadius = 15.0;
	expDamage = 0.66;
	expImpulse = 2000.0;
   repairRate     = 0;
   emap = true;
   
   thetaMin = 15;
   thetaMax = 140;

   isShielded           = true;
   energyPerDamagePoint = 50;
   maxEnergy = 150;
   rechargeRate = 0.31;
   //humSound = BaseTurretHumSound; // Was SensorHumSound
   pausePowerThread = true;

   canControl = true;
   cmdCategory = "Tactical";
   cmdIcon = CMDTurretIcon;
   cmdMiniIconName = "commander/MiniIcons/com_turretbase_grey";
   targetNameTag = 'Base';
   targetTypeTag = 'Turret';
   sensorData = TurretBaseSensorObj;
   sensorRadius = TurretBaseSensorObj.detectRadius;
   sensorColor = "0 212 45";

   firstPersonOnly = true;

   debrisShapeName = "debris_generic.dts";
   debris = TurretDebris;


};

function t1ElfTurret::onAdd(%data, %obj){
   Parent::onAdd(%data, %obj);
   %obj.mountImage(%data.barrel, 0, true);
}

datablock AudioProfile(t1FlyerEngineSound)
{
   filename    = "t1sounds/flyer_idle.wav";
   description = AudioDefaultLooping3d;
};

datablock AudioProfile(t1FlyThrustSound)
{
   filename    = "t1sounds/flyer_fly.wav";
   description = AudioDefaultLooping3d;
};

$Vehiclemax[T1ScoutFlyer]     = 4;
datablock FlyingVehicleData(T1ScoutFlyer) : ShrikeDamageProfile
{
   spawnOffset = "0 0 2";

   catagory = "Vehicles";
   shapeFile = "t1flyer.dts";
   multipassenger = false;
   computeCRC = true;

   debrisShapeName = "vehicle_air_scout_debris.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;

   drag    = 0.15;
   density = 1.0;

   mountPose[0] = scoutRoot;
   numMountPoints = 1;  
   isProtectedMountPoint[0] = true;
   cameraMaxDist = 15;
   cameraOffset = 2.5;
   cameraLag = 0.9;
   explosion = VehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   maxDamage = 1.40;
   destroyedLevel = 1.40;

   isShielded = true;
   energyPerDamagePoint = 160;
   maxEnergy = 280;      // Afterburner and any energy weapon pool
   rechargeRate = 0.8;

   minDrag = 30;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 900;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   maxAutoSpeed = 15;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 400;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 300;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.95;      // Dampen control input so you don't` whack out at very slow speeds
   
   // Maneuvering
   maxSteeringAngle = 5;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 6;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 4;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 3000;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 1200;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 400;      // Steering jets (how much you heel over when you turn)
   rollForce = 4;                // Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 4;        // Height off the ground at rest
   createHoverHeight = 4;  // Height off the ground when created
   maxForwardSpeed = 100;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 2000;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 28;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 2.8;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 4.0; // z0dd - ZOD, 9/8/02. Was 3.0

   // Rigid body
   mass = 150;        // Mass of the vehicle
   bodyFriction = 0;     // Don't mess with this.
   bodyRestitution = 0.5;   // When you hit the ground, how much you rebound. (between 0 and 1)
   minRollSpeed = 0;     // Don't mess with this.
   softImpactSpeed = 14;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 25;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 10;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.06;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 23.0;
   collDamageMultiplier   = 0.02;

   //
   minTrailSpeed = 15;      // The speed your contrail shows up at.
   trailEmitter = ContrailEmitter;
   forwardJetEmitter = WildcatJetEmitter;
   downJetEmitter = "";

   //
   jetSound = t1FlyThrustSound;
   engineSound = t1FlyerEngineSound;
   softImpactSound = SoftImpactSound;
   hardImpactSound = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;
   
   //
   softSplashSoundVelocity = 10.0; 
   mediumSplashSoundVelocity = 15.0;   
   hardSplashSoundVelocity = 20.0;   
   exitSplashSoundVelocity = 10.0;
   
   exitingWater      = VehicleExitWaterMediumSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeMediumSplashSound; 
    
   dustEmitter = VehicleLiftoffDustEmitter;
   triggerDustHeight = 4.0;
   dustHeight = 1.0;

   damageEmitter[0] = LightDamageSmoke;
   damageEmitter[1] = HeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "0.0 -3.0 0.0 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 1;

   
   minMountDist = 4;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'Scout';
   targetTypeTag = 'Turbograv';
   sensorData = AWACPulseSensor;
   sensorRadius = AWACPulseSensor.detectRadius;
   sensorColor = "255 194 9";
   
   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;
//   runningLight[1] = ShrikeLight2;

   shieldEffectScale = "0.937 1.125 0.60";

};
datablock AudioProfile(T1ScoutFireSound)
{
   filename    = "t1sounds/flierRocket.wav";
   description = AudioDefault3d;
   preload = true;
};

function T1ScoutFlyer::onAdd(%this, %obj)
{
   Parent::onAdd(%this, %obj);
   %obj.mountImage(T1ScoutMissileParam, 0);
   %obj.mountImage(T1ScoutMissileImage, 2);
   %obj.mountImage(T1ScoutMissilePairImage, 3);
   %obj.nextWeaponFire = 2;
}

datablock ParticleData(T1MissileSmokeParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = -0.02;
   inheritedVelFactor   = 0.1;

   lifetimeMS           = 1200;
   lifetimeVarianceMS   = 100;

   textureName          = "particleTest";

   useInvAlpha = false;
   spinRandomMin = -90.0;
   spinRandomMax = 90.0;

   colors[0]     = "1.0 0.75 0.0 0.0";
   colors[1]     = "0.5 0.5 0.5 1.0";
   colors[2]     = "0.3 0.3 0.3 0.0";
   sizes[0]      = 1;
   sizes[1]      = 2;
   sizes[2]      = 3;
   times[0]      = 0.0;
   times[1]      = 0.1;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(T1MissileSmokeEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;

   ejectionVelocity = 1.5;
   velocityVariance = 0.3;

   thetaMin         = 0.0;
   thetaMax         = 50.0;  

   particles = "T1MissileSmokeParticle";
};
datablock ExplosionData(ScoutMissileExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile   = T1DiscExpSound;
   particleEmitter = MissileExplosionSmokeEmitter;
   particleDensity = 150;
   particleRadius = 1.25;
   faceViewer = true;

   sizes[0] = "1.0 1.0 1.0";
   sizes[1] = "1.0 1.0 1.0";
   times[0] = 0.0;
   times[1] = 1.5;
};
datablock LinearProjectileData(T1ScoutRocket)
{
   projectileShapeName = "weapon_missile_projectile.dts";
   emitterDelay = -1;
   baseEmitter = T1MissileSmokeEmitter;
   delayEmitter = MissileFireEmitter;
   bubbleEmitter = GrenadeBubbleEmitter;
   bubbleEmitTime = 1.0;
   directDamage = 0.0;
   hasDamageRadius = true;
   indirectDamage = 0.25;
   damageRadius = 7.5;
   radiusDamageType = $DamageType::Missile;
   kickBackStrength = 1000;
   sound = "";
   explosion = "ScoutMissileExplosion";
   underwaterExplosion = "UnderwaterDiscExplosion";
   splash = MissileSplash;
   dryVelocity = 90;
   wetVelocity = 50;
   velInheritFactor = 1.0;
   fizzleTimeMS = 5000;
   lifetimeMS = 5000;
   explodeOnDeath = true;
   reflectOnWaterImpactAngle = 15.0;
   explodeOnWaterImpact = false;
   deflectionOnWaterImpact = 20.0;
   fizzleUnderwaterMS = 5000;
//   activateDelayMS = 200;
   hasLight = true;
   lightRadius = 6.0;
   lightColor = "0.175 0.175 0.5";
};

datablock ShapeBaseImageData(T1ScoutMissilePairImage)
{
   className = WeaponImage;
   shapeFile = "weapon_energy_vehicle.dts";
   item      = Chaingun;
   ammo   = ChaingunAmmo;
   projectile = T1ScoutRocket;
   projectileType = LinearProjectile;
   mountPoint = 10;
   offset = "0.8 -0.1 0.1";
   
   usesEnergy = true;
   useMountEnergy = true;
   // DAVEG -- balancing numbers below!
   minEnergy = 5;
   fireEnergy = 5;
   fireTimeout = 500;

   stateName[0]                     = "Activate";
   stateTransitionOnTimeout[0]      = "ActivateReady";
   stateTimeoutValue[0]             = 0.5;
   stateSequence[0]                 = "Activate";
   stateSound[0]                    = MissileSwitchSound;

   stateName[1]                     = "ActivateReady";
   stateTransitionOnLoaded[1]       = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "CheckTarget";

   stateName[3]                     = "Fire";
   stateTransitionOnTimeout[3]      = "Reload";
   stateTimeoutValue[3]             = 1.0;
   stateFire[3]                     = true;
   stateRecoil[3]                   = LightRecoil;
   stateAllowImageChange[3]         = false;
   stateSequence[3]                 = "Fire";
   stateScript[3]                   = "onFire";
   stateSound[3]                    = T1ScoutFireSound;

   stateName[4]                     = "Reload";
   stateTransitionOnNoAmmo[4]       = "NoAmmo";
   stateTransitionOnTimeout[4]      = "Ready";
   stateTimeoutValue[4]             = 1.0;
   stateAllowImageChange[4]         = false;
   stateSequence[4]                 = "Reload";
   //stateSound[4]                    = MissileReloadSound;

   stateName[5]                     = "NoAmmo";
   stateTransitionOnAmmo[5]         = "Reload";
   stateSequence[5]                 = "NoAmmo";
   stateTransitionOnTriggerDown[5]  = "Fire"; // Was DryFire

   stateName[6]                     = "DryFire";
   //stateSound[6]                    = "MissileDryFireSound";
   stateTimeoutValue[6]             = 1.0;
   stateTransitionOnTimeout[6]      = "ActivateReady";
   
   stateName[7]                     = "CheckTarget";
   stateTransitionOnNoTarget[7]     = "Fire"; // Was DryFire
   stateTransitionOnTarget[7]       = "Fire";
};

datablock ShapeBaseImageData(T1ScoutMissileImage) : T1ScoutMissilePairImage
{
   offset = "-0.8 -0.1 0.1";
   //stateScript[3]           = "onTriggerDown";
   //stateScript[5]           = "onTriggerUp";
   //stateScript[6]           = "onTriggerUp";
};

datablock ShapeBaseImageData(T1ScoutMissileParam)
{
   mountPoint = 2;
   shapeFile = "turret_muzzlepoint.dts";

   projectile = T1ScoutRocket;
   projectileType = LinearProjectile;
};

function T1ScoutFlyer::onTrigger(%data, %obj, %trigger, %state)
{
   // data = Sparrow datablock
   // obj = Sparrow object number
   // trigger = 0 for "fire", 1 for "jump", 3 for "thrust"
   // state = 1 for firing, 0 for not firing
   if(%trigger == 0)
   {
      switch (%state) {
         case 0:
            %obj.fireWeapon = false;
            %obj.setImageTrigger(2, false);
            %obj.setImageTrigger(3, false);
         case 1:
            %obj.fireWeapon = true;
            if(%obj.nextWeaponFire == 2) {
               %obj.setImageTrigger(2, true);
               %obj.setImageTrigger(3, false);
            }
            else {
               %obj.setImageTrigger(2, false);
               %obj.setImageTrigger(3, true);
            }
      }
   }
}

function T1ScoutMissileImage::onFire(%data,%obj,%slot)
{
   // obj = SparrowFlyer object number
   // slot = 2

   %p = Parent::onFire(%data,%obj,%slot);
   MissileSet.add(%p);
   %obj.nextWeaponFire = 3;
   schedule(%data.fireTimeout, 0, "fireNextGun", %obj);
}

function T1ScoutMissilePairImage::onFire(%data,%obj,%slot)
{
   // obj = SparrowFlyer object number
   // slot = 3

   %p = Parent::onFire(%data,%obj,%slot);
   MissileSet.add(%p);
   %obj.nextWeaponFire = 2;
   schedule(%data.fireTimeout, 0, "fireNextGun", %obj);
}

function T1ScoutMissileImage::onTriggerDown(%this, %obj, %slot)
{
}

function T1ScoutMissileImage::onTriggerUp(%this, %obj, %slot)
{
}

function T1ScoutMissileImage::onMount(%this, %obj, %slot)
{
}

function T1ScoutMissilePairImage::onMount(%this, %obj, %slot)
{
}

function T1ScoutMissileImage::onUnmount(%this,%obj,%slot)
{
}

function T1ScoutMissilePairImage::onUnmount(%this,%obj,%slot)
{
}


function T1ScoutFlyer::playerMounted(%data, %obj, %player, %node)
{
   if(%node == 0) {
      // pilot position
	   //commandToClient(%player.client, 'setHudMode', 'Pilot', "HAPC", %node);
      commandToClient(%player.client, 'setHudMode', 'Pilot', "Shrike", %node);

   }
   else {
      // all others
	   commandToClient(%player.client, 'setHudMode', 'Passenger', "HAPC", %node);
   }
   // update observers who are following this guy...
   if( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, false );
}

function T1ScoutFlyer::playerDismounted(%data, %obj, %player)
{
   %obj.fireWeapon = false;
   %obj.setImageTrigger(2, false);
   %obj.setImageTrigger(3, false);
   setTargetSensorGroup(%obj.getTarget(), %obj.team);
}

$Vehiclemax[T12ScoutFlyer]     = 4;
datablock FlyingVehicleData(T12ScoutFlyer) : ShrikeDamageProfile
{
   spawnOffset = "0 0 2";

   catagory = "Vehicles";
   shapeFile = "t1flyer2.dts";
   multipassenger = false;
   computeCRC = true;

   debrisShapeName = "vehicle_air_scout_debris.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;

   drag    = 0.15;
   density = 1.0;

   mountPose[0] = scoutRoot;
   numMountPoints = 1;  
   isProtectedMountPoint[0] = true;
   cameraMaxDist = 15;
   cameraOffset = 2.5;
   cameraLag = 0.9;
   explosion = VehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   maxDamage = 1.40;
   destroyedLevel = 1.40;

   isShielded = true;
   energyPerDamagePoint = 160;
   maxEnergy = 280;      // Afterburner and any energy weapon pool
   rechargeRate = 0.8;

   minDrag = 30;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 900;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   maxAutoSpeed = 15;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 400;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 300;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.95;      // Dampen control input so you don't` whack out at very slow speeds
   
   // Maneuvering
   maxSteeringAngle = 5;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 6;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 4;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 3000;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 1200;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 400;      // Steering jets (how much you heel over when you turn)
   rollForce = 4;                // Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 4;        // Height off the ground at rest
   createHoverHeight = 4;  // Height off the ground when created
   maxForwardSpeed = 100;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 2000;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 28;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 2.8;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 4.0; // z0dd - ZOD, 9/8/02. Was 3.0

   // Rigid body
   mass = 150;        // Mass of the vehicle
   bodyFriction = 0;     // Don't mess with this.
   bodyRestitution = 0.5;   // When you hit the ground, how much you rebound. (between 0 and 1)
   minRollSpeed = 0;     // Don't mess with this.
   softImpactSpeed = 14;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 25;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 10;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.06;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 23.0;
   collDamageMultiplier   = 0.02;

   //
   minTrailSpeed = 15;      // The speed your contrail shows up at.
   trailEmitter = ContrailEmitter;
   forwardJetEmitter = WildcatJetEmitter;
   downJetEmitter = "";

   //
   jetSound = t1FlyThrustSound;
   engineSound = t1FlyerEngineSound;
   softImpactSound = SoftImpactSound;
   hardImpactSound = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;
   
   //
   softSplashSoundVelocity = 10.0; 
   mediumSplashSoundVelocity = 15.0;   
   hardSplashSoundVelocity = 20.0;   
   exitSplashSoundVelocity = 10.0;
   
   exitingWater      = VehicleExitWaterMediumSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeMediumSplashSound; 
    
   dustEmitter = VehicleLiftoffDustEmitter;
   triggerDustHeight = 4.0;
   dustHeight = 1.0;

   damageEmitter[0] = LightDamageSmoke;
   damageEmitter[1] = HeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "0.0 -3.0 0.0 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 1;

   //
   max[chaingunAmmo] = 1000;

   minMountDist = 4;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'Scout';
   targetTypeTag = 'Turbograv';
   sensorData = AWACPulseSensor;
   sensorRadius = AWACPulseSensor.detectRadius;
   sensorColor = "255 194 9";
   
   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;
//   runningLight[1] = ShrikeLight2;

   shieldEffectScale = "0.937 1.125 0.60";

};

function T12ScoutFlyer::playerMounted(%data, %obj, %player, %node)
{
   if(%node == 0) {
      // pilot position
	   commandToClient(%player.client, 'setHudMode', 'Pilot', "HAPC", %node);
   }
   else {
      // all others
	   commandToClient(%player.client, 'setHudMode', 'Passenger', "HAPC", %node);
   }
   // update observers who are following this guy...
   if( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, false );
}

$Vehiclemax[T1LAPCFlyer]     = 2;
datablock FlyingVehicleData(T1LAPCFlyer) : HavocDamageProfile
{
   spawnOffset = "0 0 6";
   renderWhenDestroyed = false;

   catagory = "Vehicles";
   shapeFile = "t1lpc.dts";
   multipassenger = true;
   computeCRC = true;


   debrisShapeName = "vehicle_air_hapc_debris.dts";
   debris = ShapeDebris;

   drag = 0.2;
   density = 1.0;

   mountPose[0] = root;
//   mountPose[1] = sitting;
   numMountPoints = 3;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = true;
   isProtectedMountPoint[2] = true;


   cameraMaxDist = 17;
   cameraOffset = 2;
   cameraLag = 8.5;
   explosion = LargeAirVehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   maxDamage = 3.50;
   destroyedLevel = 3.50;

   isShielded = true;
   rechargeRate = 1.45; // z0dd - ZOD, 4/16/02. Was 0.8
   energyPerDamagePoint = 150; // z0dd - ZOD, 4/16/02. Was 200
   maxEnergy = 800; // z0dd - ZOD, 4/16/02. Was 550
   minDrag = 100;                // Linear Drag
   rotationalDrag = 2700;        // Anguler Drag

   // Auto stabilize speed
   maxAutoSpeed = 10;
   autoAngularForce = 3000;      // Angular stabilizer force
   autoLinearForce = 450;        // Linear stabilzer force
   autoInputDamping = 0.95;      // 
                                                        
   // Maneuvering
   maxSteeringAngle = 8;
   horizontalSurfaceForce = 10;  // Horizontal center "wing"
   verticalSurfaceForce = 10;    // Vertical center "wing"
   maneuveringForce = 6250;      // Horizontal jets // z0dd - ZOD, 4/25/02. Was 6000
   steeringForce = 1000;          // Steering jets
   steeringRollForce = 400;      // Steering jets
   rollForce = 12;               // Auto-roll
   hoverHeight = 4;         // Height off the ground at rest
   createHoverHeight = 4;   // Height off the ground when created
   maxForwardSpeed = 80;  // speed in which forward thrust force is no longer applied (meters/second) z0dd - ZOD, 4/25/02. Was 71

   // Turbo Jet
   jetForce = 5750; // z0dd - ZOD, 4/25/02. Was 5000
   minJetEnergy = 55;
   jetEnergyDrain = 4.5; // z0dd - ZOD, 4/16/02. Was 3.6
   vertThrustMultiple = 3.0;

   dustEmitter = LargeVehicleLiftoffDustEmitter;
   triggerDustHeight = 4.0;
   dustHeight = 2.0;

   damageEmitter[0] = LightDamageSmoke;
   damageEmitter[1] = HeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "3.0 -3.0 0.0 ";
   damageEmitterOffset[1] = "-3.0 -3.0 0.0 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 2;

   // Rigid body
   mass = 550;
   bodyFriction = 0;
   bodyRestitution = 0.3;
   minRollSpeed = 0;
   softImpactSpeed = 12;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 15;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 25;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.060;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 28;
   collDamageMultiplier   = 0.020;

   //
   minTrailSpeed = 15;
   trailEmitter = ContrailEmitter;
   forwardJetEmitter = FlyerJetEmitter;
   downJetEmitter = FlyerJetEmitter;

   //
   jetSound = t1FlyThrustSound;
   engineSound = t1FlyerEngineSound;
   softImpactSound = SoftImpactSound;
   hardImpactSound = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 5.0; 
   mediumSplashSoundVelocity = 8.0;   
   hardSplashSoundVelocity = 12.0;   
   exitSplashSoundVelocity = 8.0;
   
   exitingWater      = VehicleExitWaterHardSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterHardSound;
   waterWakeSound    = VehicleWakeHardSplashSound; 
   
   minMountDist = 4;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingHAPCIcon;
   cmdMiniIconName = "commander/MiniIcons/com_hapc_grey";
   targetNameTag = 'LPC';
   targetTypeTag = 'Light Transport';
   // z0dd - ZOD, 5/07/04. Stealth HAPC if gameplay changes in affect.
   sensorData = $Host::ClassicLoadHavocChanges ? HAPCSensor : VehiclePulseSensor;
   sensorRadius = VehiclePulseSensor.detectRadius; // z0dd - ZOD, 4/25/02. Allows sensor to be shown on CC

   checkRadius = 7.8115;
   observeParameters = "1 15 15";

   stuckTimerTicks = 32;   // If the hapc spends more than 1 sec in contact with something
   stuckTimerAngle = 80;   //  with a > 80 deg. pitch, BOOM!

   shieldEffectScale = "1.0 0.9375 0.45";
};
function T1LAPCFlyer::playerMounted(%data, %obj, %player, %node)
{
   if(%node == 0) {
      // pilot position
	   commandToClient(%player.client, 'setHudMode', 'Pilot', "HAPC", %node);
   }
   else {
      // all others
	   commandToClient(%player.client, 'setHudMode', 'Passenger', "HAPC", %node);
   }

   // update observers who are following this guy...
   if( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, false );
}
$Vehiclemax[T1HAPCFlyer]     = 1;
datablock FlyingVehicleData(T1HAPCFlyer) : HavocDamageProfile
{
   spawnOffset = "0 0 6";
   renderWhenDestroyed = false;

   catagory = "Vehicles";
   shapeFile = "t1hpc.dts";
   multipassenger = true;
   computeCRC = true;


   debrisShapeName = "vehicle_air_hapc_debris.dts";
   debris = ShapeDebris;

   drag = 0.2;
   density = 1.0;

   mountPose[0] = root;
//   mountPose[1] = sitting;
   numMountPoints = 5;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = true;
   isProtectedMountPoint[2] = true;
   isProtectedMountPoint[3] = true;
   isProtectedMountPoint[4] = true;

   cameraMaxDist = 17;
   cameraOffset = 2;
   cameraLag = 8.5;
   explosion = LargeAirVehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   maxDamage = 3.50;
   destroyedLevel = 3.50;

   isShielded = true;
   rechargeRate = 1.45; // z0dd - ZOD, 4/16/02. Was 0.8
   energyPerDamagePoint = 150; // z0dd - ZOD, 4/16/02. Was 200
   maxEnergy = 800; // z0dd - ZOD, 4/16/02. Was 550
   minDrag = 100;                // Linear Drag
   rotationalDrag = 2700;        // Anguler Drag

   // Auto stabilize speed
   maxAutoSpeed = 10;
   autoAngularForce = 3000;      // Angular stabilizer force
   autoLinearForce = 450;        // Linear stabilzer force
   autoInputDamping = 0.95;      // 
                                                        
   // Maneuvering
   maxSteeringAngle = 8;
   horizontalSurfaceForce = 10;  // Horizontal center "wing"
   verticalSurfaceForce = 10;    // Vertical center "wing"
   maneuveringForce = 6250;      // Horizontal jets // z0dd - ZOD, 4/25/02. Was 6000
   steeringForce = 1000;          // Steering jets
   steeringRollForce = 400;      // Steering jets
   rollForce = 12;               // Auto-roll
   hoverHeight = 4;         // Height off the ground at rest
   createHoverHeight = 4;   // Height off the ground when created
   maxForwardSpeed = 80;  // speed in which forward thrust force is no longer applied (meters/second) z0dd - ZOD, 4/25/02. Was 71

   // Turbo Jet
   jetForce = 5750; // z0dd - ZOD, 4/25/02. Was 5000
   minJetEnergy = 55;
   jetEnergyDrain = 4.5; // z0dd - ZOD, 4/16/02. Was 3.6
   vertThrustMultiple = 3.0;

   dustEmitter = LargeVehicleLiftoffDustEmitter;
   triggerDustHeight = 4.0;
   dustHeight = 2.0;

   damageEmitter[0] = LightDamageSmoke;
   damageEmitter[1] = HeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "3.0 -3.0 0.0 ";
   damageEmitterOffset[1] = "-3.0 -3.0 0.0 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 2;

   // Rigid body
   mass = 550;
   bodyFriction = 0;
   bodyRestitution = 0.3;
   minRollSpeed = 0;
   softImpactSpeed = 12;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 15;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 25;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.060;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 28;
   collDamageMultiplier   = 0.020;

   //
   minTrailSpeed = 15;
   trailEmitter = ContrailEmitter;
   forwardJetEmitter = FlyerJetEmitter;
   downJetEmitter = FlyerJetEmitter;

   //
   jetSound = t1FlyThrustSound;
   engineSound = t1FlyerEngineSound;
   softImpactSound = SoftImpactSound;
   hardImpactSound = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 5.0; 
   mediumSplashSoundVelocity = 8.0;   
   hardSplashSoundVelocity = 12.0;   
   exitSplashSoundVelocity = 8.0;
   
   exitingWater      = VehicleExitWaterHardSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterHardSound;
   waterWakeSound    = VehicleWakeHardSplashSound; 
   
   minMountDist = 4;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingHAPCIcon;
   cmdMiniIconName = "commander/MiniIcons/com_hapc_grey";
   targetNameTag = 'HPC';
   targetTypeTag = 'Heavy Transport';
   // z0dd - ZOD, 5/07/04. Stealth HAPC if gameplay changes in affect.
   sensorData = $Host::ClassicLoadHavocChanges ? HAPCSensor : VehiclePulseSensor;
   sensorRadius = VehiclePulseSensor.detectRadius; // z0dd - ZOD, 4/25/02. Allows sensor to be shown on CC

   checkRadius = 7.8115;
   observeParameters = "1 15 15";

   stuckTimerTicks = 32;   // If the hapc spends more than 1 sec in contact with something
   stuckTimerAngle = 80;   //  with a > 80 deg. pitch, BOOM!

   shieldEffectScale = "1.0 0.9375 0.45";
};

function T1HAPCFlyer::playerMounted(%data, %obj, %player, %node)
{
   if(%node == 0) {
      // pilot position
	   commandToClient(%player.client, 'setHudMode', 'Pilot', "HAPC", %node);
   }
   else {
      // all others
	   commandToClient(%player.client, 'setHudMode', 'Passenger', "HAPC", %node);
   }

   // update observers who are following this guy...
   if( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, false );
}

datablock StaticShapeData(T1InvyDeployableObj) : StaticShapeDamageProfile
{
   className = Station;
   shapeFile = "t1DepInvy.dts";
   maxDamage = 0.70;
   destroyedLevel = 0.70;
   disabledLevel = 0.42;
   explosion      = DeployablesExplosion;
   expDmgRadius = 8.0;
   expDamage = 0.35;
   expImpulse = 500.0;

   dynamicType = $TypeMasks::StationObjectType;
   isShielded = true;
   energyPerDamagePoint = 110;
   maxEnergy = 50;
   rechargeRate = 0.20;
   renderWhenDestroyed = false;
   doesRepair = true;

   deployedObject = true;

   cmdCategory = "DSupport";
   cmdIcon = CMDStationIcon;
   cmdMiniIconName = "commander/MiniIcons/com_inventory_grey";
   targetNameTag = 'Deployable';
   targetTypeTag = 'Station';

   debrisShapeName = "debris_generic_small.dts";
   debris = DeployableDebris;
   heatSignature = 0;
};

datablock ShapeBaseImageData(T1InvyDeployableImage)
{
   mass = 12; // z0dd - ZOD, 7/17/02. large packs are too heavy enough with new physics. was 15
   emap = true;

   shapeFile = "t1DepInvy_Pack.dts";
   item = T1InvyDeployable;
   mountPoint = 1;
   offset = "0 -0.2 -0.8";
   rotation = "0 0 1 180";
   deployed = T1InvyDeployableObj;
   heatSignature = 0;

   stateName[0] = "Idle";
   stateTransitionOnTriggerDown[0] = "Activate";

   stateName[1] = "Activate";
   stateScript[1] = "onActivate";
   stateTransitionOnTriggerUp[1] = "Idle";

   isLarge = true;
   maxDepSlope = 30;
   deploySound = StationDeploySound;

   flatMinDeployDis   = 2.0; // z0dd - ZOD, 5/18/03. Was 1.0, try to prevent it intersecting with plyr bb.
   flatMaxDeployDis   = 5.0;

   minDeployDis       = 3.0; // z0dd - ZOD, 5/18/03. Was 2.5, try to prevent it intersecting with plyr bb.
   maxDeployDis       = 6.0;
};

$TeamDeployableMax[T1InvyDeployable] = 4;
$TeamDeployableMin[T1InvyDeployable] = 4;

datablock ItemData(T1InvyDeployable)
{
   className = Pack;
   catagory = "Deployables";
   shapeFile = "t1DepInvy_Pack.dts";
   mass = 3.0;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 1;
   rotate = false;
   image = "T1InvyDeployableImage";
   pickUpName = "an inventory station pack";
   heatSignature = 0;

   computeCRC = true;
   emap = true;

};

function T1InvyDeployableImage::getInitialRotation(%item, %plyr) {
   %rot = rotFromTransform(%plyr.getTransform());
   // Rotate 180 degrees around Z-axis (PI radians)
   // Multiply original rotation by the 180-degree Z rotation
   %newRot = MatrixMultiply("0 0 0" SPC %rot, "0 0 0" SPC "0 0 1" SPC 3.14159265359);
   return getWords(rotFromTransform(%newRot), 0, 3);
}

function T1InvyDeployableImage::onDeploy(%item, %plyr, %slot){
   %obj = Parent::onDeploy(%item, %plyr, %slot);
   %obj.init = 0;
   %trigger = new Trigger()
   { 
      dataBlock = t1StationTrigger;
      polyhedron = "-0.125 0.0 0.1 0.25 0.0 0.0 0.0 -0.8 0.0 0.0 0.0 1.0";
   };     

   MissionCleanup.add(%trigger);

   %trans = %obj.getTransform();
   %vSPos = getWords(%trans,0,2);
   %vRot =  getWords(%trans,3,5);
   %vAngle = getWord(%trans,6);
   %matrix = VectorOrthoBasis(%vRot @ " " @ %vAngle + 0.0);
   %yRot = getWords(%matrix, 3, 5);
   %pos = vectorAdd(%vSPos, vectorScale(%yRot, 1));

   %trigger.setTransform(%pos @ " " @ %vRot @ " " @ %vAngle);

   // associate the trigger with the station
   %trigger.station =%obj;
   %obj.trigger = %trigger;


   %b = new BeaconObject() {
      dataBlock = "DeployedBeacon";
      position =%obj.position;
      rotation = %obj.rotation;
      team = %obj.team;
      scale = "0.5 0.5 0.5";
      invincible = "1";
   };
   MissionCleanup.add(%b);
   %b.setBeaconType(friend);
   %b.setTarget(%obj.team);
   %obj.beacon = %b;
}
function T1InvyDeployable::onCollision(%data,%obj,%col){
   if (%col.getDataBlock().className $= Armor && %col.getState() !$= "Dead" && %col.getMountedImage(2) == 0 && !%col.isMounted()){
      if (%col.client){
         messageClient(%col.client, 'MsgItemPickup', '\c0You picked up %1.', %data.pickUpName);
         serverPlay3D(ItemPickupSound, %col.getTransform());
      }
      if (%obj.isStatic()){
         %obj.respawn();
      }
      else{
         %obj.delete();
      }
      %col.setInventory(T1InvyDeployable, 1, true);
   }
}

function T1InvyDeployableObj::onDestroyed(%this, %obj, %prevState){
   Parent::onDestroyed(%this, %obj, %prevState);
   $TeamDeployedCount[%obj.team, T1InvyDeployable]--;
   if(isObject(%obj.trigger)){   
      %obj.trigger.delete();
   }
   if(isObject(%obj.beacon)){   
      %obj.beacon.delete();
   }
   %obj.schedule(500, "delete");
}


datablock StaticShapeData(T1AmmoDeployableObj) : StaticShapeDamageProfile
{
   className = Station;
   shapeFile = "t1DepAmmo.dts";
   maxDamage = 0.70;
   destroyedLevel = 0.70;
   disabledLevel = 0.42;
   explosion      = DeployablesExplosion;
   expDmgRadius = 8.0;
   expDamage = 0.35;
   expImpulse = 500.0;

   dynamicType = $TypeMasks::StationObjectType;
   isShielded = true;
   energyPerDamagePoint = 110;
   maxEnergy = 50;
   rechargeRate = 0.20;
   renderWhenDestroyed = false;
   doesRepair = true;

   deployedObject = true;

   cmdCategory = "DSupport";
   cmdIcon = CMDStationIcon;
   cmdMiniIconName = "commander/MiniIcons/com_inventory_grey";
   targetNameTag = 'Ammo';
   targetTypeTag = 'Station';

   debrisShapeName = "debris_generic_small.dts";
   debris = DeployableDebris;
   heatSignature = 0;
};

datablock ShapeBaseImageData(T1AmmoDeployableImage)
{
   mass = 12; // z0dd - ZOD, 7/17/02. large packs are too heavy enough with new physics. was 15
   emap = true;

   shapeFile = "t1DepAmmo.dts";
   item = T1AmmoDeployable;
   mountPoint = 1;
   offset = "0 -0.2 -0.3";
   rotation = "0 0 1 180";
   deployed = T1AmmoDeployableObj;
   heatSignature = 0;

   stateName[0] = "Idle";
   stateTransitionOnTriggerDown[0] = "Activate";

   stateName[1] = "Activate";
   stateScript[1] = "onActivate";
   stateTransitionOnTriggerUp[1] = "Idle";

   isLarge = true;
   maxDepSlope = 30;
   deploySound = StationDeploySound;

   flatMinDeployDis   = 2.0; // z0dd - ZOD, 5/18/03. Was 1.0, try to prevent it intersecting with plyr bb.
   flatMaxDeployDis   = 5.0;

   minDeployDis       = 3.0; // z0dd - ZOD, 5/18/03. Was 2.5, try to prevent it intersecting with plyr bb.
   maxDeployDis       = 6.0;
};

$TeamDeployableMax[T1AmmoDeployable] = 4;
$TeamDeployableMin[T1AmmoDeployable] = 4;

datablock ItemData(T1AmmoDeployable)
{
   className = Pack;
   catagory = "Deployables";
   shapeFile = "t1DepAmmo.dts";
   mass = 3.0;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 1;
   rotate = false;
   image = "T1AmmoDeployableImage";
   pickUpName = "an ammo station pack";
   heatSignature = 0;

   computeCRC = true;
   emap = true;

};

function T1AmmoDeployableImage::getInitialRotation(%item, %plyr) {
   %rot = rotFromTransform(%plyr.getTransform());
   // Rotate 180 degrees around Z-axis (PI radians)
   // Multiply original rotation by the 180-degree Z rotation
   %newRot = MatrixMultiply("0 0 0" SPC %rot, "0 0 0" SPC "0 0 1" SPC 3.14159265359);
   return getWords(rotFromTransform(%newRot), 0, 3);
}

function T1AmmoDeployableImage::onDeploy(%item, %plyr, %slot){
   %obj = Parent::onDeploy(%item, %plyr, %slot);
   %obj.init = 0;
   %trigger = new Trigger()
   { 
      dataBlock = t1StationTrigger;
      polyhedron = "-0.125 0.0 0.1 0.25 0.0 0.0 0.0 -0.8 0.0 0.0 0.0 1.0";
   };     

   MissionCleanup.add(%trigger);

   %trans = %obj.getTransform();
   %vSPos = getWords(%trans,0,2);
   %vRot =  getWords(%trans,3,5);
   %vAngle = getWord(%trans,6);
   %matrix = VectorOrthoBasis(%vRot @ " " @ %vAngle + 0.0);
   %yRot = getWords(%matrix, 3, 5);
   %pos = vectorAdd(%vSPos, vectorScale(%yRot, 1));

   %trigger.setTransform(%pos @ " " @ %vRot @ " " @ %vAngle);

   // associate the trigger with the station
   %trigger.station =%obj;
   %obj.trigger = %trigger;

}
function T1AmmoDeployable::onCollision(%data,%obj,%col){
   if (%col.getDataBlock().className $= Armor && %col.getState() !$= "Dead" && %col.getMountedImage(2) == 0 && !%col.isMounted()){
      if (%col.client){
         messageClient(%col.client, 'MsgItemPickup', '\c0You picked up %1.', %data.pickUpName);
         serverPlay3D(ItemPickupSound, %col.getTransform());
      }
      if (%obj.isStatic()){
         %obj.respawn();
      }
      else{
         %obj.delete();
      }
      %col.setInventory(T1AmmoDeployable, 1, true);
      
   }
}

function T1AmmoDeployableObj::onDestroyed(%this, %obj, %prevState){
   Parent::onDestroyed(%this, %obj, %prevState);
   $TeamDeployedCount[%obj.team, T1AmmoDeployable]--;
   if(isObject(%obj.trigger)){   
      %obj.trigger.delete();
   }
   %obj.schedule(500, "delete");
}


datablock LinearFlareProjectileData(T1RemoteTurretBolt)
{
   directDamage        = 0.14;
   directDamageType    = $DamageType::IndoorDepTurret;
   explosion           = T1BlasterExplosion;
   kickBackStrength  = 0.0;

   dryVelocity       = 120.0;
   wetVelocity       = 40.0;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 2000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   numFlares         = 20;
   size              = 0.45;
   flareColor        = "1 0.35 0.35";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound = T1BlasterExpSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "1 0.35 0.35";
};

datablock TurretImageData(T1RemoteBarrel)
{
   shapeFile = "turret_muzzlepoint.dts";
   item      = TurretIndoorDeployable; // z0dd - ZOD, 4/25/02. Was wrong: IndoorTurretBarrel

   projectile = T1RemoteTurretBolt;
   projectileType = LinearFlareProjectile;

   usesEnergy = true;
   fireEnergy = 4.5;
   minEnergy = 4.5;

   lightType = "WeaponFireLight";
   lightColor = "0.25 0.15 0.15 1.0";
   lightTime = "1000";
   lightRadius = "2";

   muzzleFlash = IndoorTurretMuzzleFlash;

   // Turret parameters
   activationMS      = 105; // z0dd - ZOD, 3/27/02. Was 150. Amount of time it takes turret to unfold
   deactivateDelayMS = 300;
   thinkTimeMS       = 105; // z0dd - ZOD, 3/27/02. Was 150. Amount of time before turret starts to unfold (activate)
   degPerSecTheta    = 580;
   degPerSecPhi      = 960;
   attackRadius      = 60;

   // State transitions
   stateName[0]                  = "Activate";
   stateTransitionOnNotLoaded[0] = "Dead";
   stateTransitionOnLoaded[0]    = "ActivateReady";

   stateName[1]                  = "ActivateReady";
   stateSequence[1]              = "Activate";
   //stateSound[1]                 = IBLSwitchSound;
   stateTimeoutValue[1]          = 1;
   stateTransitionOnTimeout[1]   = "Ready";
   stateTransitionOnNotLoaded[1] = "Deactivate";
   stateTransitionOnNoAmmo[1]    = "NoAmmo";

   stateName[2]                    = "Ready";
   stateTransitionOnNotLoaded[2]   = "Deactivate";
   stateTransitionOnTriggerDown[2] = "Fire";
   stateTransitionOnNoAmmo[2]      = "NoAmmo";

   stateName[3]                = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3]        = 0.3;
   stateFire[3]                = true;
   stateShockwave[3]           = true;
   stateRecoil[3]              = LightRecoil;
   stateAllowImageChange[3]    = false;
   stateSequence[3]            = "Fire";
   stateSound[3]               = T1BlasterFireSound;
   stateScript[3]              = "onFire";

   stateName[4]                  = "Reload";
   stateTimeoutValue[4]          = 0.8;
   stateAllowImageChange[4]      = false;
   stateSequence[4]              = "Reload";
   stateTransitionOnTimeout[4]   = "Ready";
   stateTransitionOnNotLoaded[4] = "Deactivate";
   stateTransitionOnNoAmmo[4]    = "NoAmmo";

   stateName[5]                = "Deactivate";
   stateSequence[5]            = "Activate";
   stateDirection[5]           = false;
   stateTimeoutValue[5]        = 1;
   stateTransitionOnLoaded[5]  = "ActivateReady";
   stateTransitionOnTimeout[5] = "Dead";

   stateName[6]               = "Dead";
   stateTransitionOnLoaded[6] = "ActivateReady";

   stateName[7]             = "NoAmmo";
   stateTransitionOnAmmo[7] = "Reload";
   stateSequence[7]         = "NoAmmo";
};


datablock TurretData(T1RemoteTurretObj) : TurretDamageProfile
{
   catagory = "Turrets";
   className = DeployedTurret;
   shapeFile = "t1RemoteTurret.dts";

   rechargeRate = 0.15;
   humSound = DeployedTurretHumSound; // z0dd - ZOD, 5/18/03. New sound
   mass = 5.0;
   maxDamage = 0.80;
   destroyedLevel = 0.80;
   disabledLevel = 0.35;
   explosion      = HandGrenadeExplosion;  
   expDmgRadius = 5.0;
   expDamage = 0.3;
   expImpulse = 500.0;
   repairRate = 0;
	
   deployedObject = true;

   thetaMin = 0; 
   thetaMax = 145;
   thetaNull = 90;

   yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances

   isShielded = true;
   energyPerDamagePoint = 110;
   maxEnergy = 60;
   renderWhenDestroyed = false;
   barrel = T1RemoteBarrel;
   heatSignature = 0;

   canControl = true;
   cmdCategory = "DTactical";
   cmdIcon = CMDTurretIcon;
   cmdMiniIconName = "commander/MiniIcons/com_turret_grey";
   targetNameTag = 'Remote';
   targetTypeTag = 'Turret';
   sensorData = DeployedOutdoorTurretSensor;
   sensorRadius = DeployedOutdoorTurretSensor.detectRadius;
   sensorColor = "191 0 226";

   firstPersonOnly = true;

   debrisShapeName = "debris_generic_small.dts";
   debris = TurretDebrisSmall;
};

datablock ShapeBaseImageData(T1RemoteTurretImage)
{
   mass = 12; // z0dd - ZOD, 7/17/02. large packs are too heavy enough with new physics. was 15
   emap = true;

   shapeFile = "t1RemoteTurret_Pack.dts";
   item = T1RemoteTurret;
   mountPoint = 1;
   offset = "0 -0.2 -0.5";
   rotation = "0 0 1 180";
   deployed = T1RemoteTurretObj;
   heatSignature = 0;

   stateName[0] = "Idle";
   stateTransitionOnTriggerDown[0] = "Activate";

   stateName[1] = "Activate";
   stateScript[1] = "onActivate";
   stateTransitionOnTriggerUp[1] = "Idle";

   isLarge = true;
   maxDepSlope = 30;
   deploySound = StationDeploySound;

   flatMinDeployDis   = 2.0; // z0dd - ZOD, 5/18/03. Was 1.0, try to prevent it intersecting with plyr bb.
   flatMaxDeployDis   = 5.0;

   minDeployDis       = 3.0; // z0dd - ZOD, 5/18/03. Was 2.5, try to prevent it intersecting with plyr bb.
   maxDeployDis       = 6.0;
};

$TeamDeployableMax[T1RemoteTurret] = 6;
$TeamDeployableMin[T1RemoteTurret] = 6;

datablock ItemData(T1RemoteTurret)
{
   className = Pack;
   catagory = "Deployables";
   shapeFile = "t1RemoteTurret_Pack.dts";
   mass = 3.0;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 1;
   rotate = false;
   image = "T1RemoteTurretImage";
   pickUpName = "an ammo station pack";
   heatSignature = 0;

   computeCRC = true;
   emap = true;

};
function T1RemoteTurretObj::onAdd(%data, %obj){
   Parent::onAdd(%data, %obj);
   %obj.mountImage(%data.barrel, 0, true);
}

function T1RemoteTurret::onCollision(%data,%obj,%col){
   if (%col.getDataBlock().className $= Armor && %col.getState() !$= "Dead" && %col.getMountedImage(2) == 0 && !%col.isMounted()){
      if (%col.client){
         messageClient(%col.client, 'MsgItemPickup', '\c0You picked up %1.', %data.pickUpName);
         serverPlay3D(ItemPickupSound, %col.getTransform());
      }
      if (%obj.isStatic()){
         %obj.respawn();
      }
      else{
         %obj.delete();
      }
      %col.setInventory(T1RemoteTurret, 1, true);
      
   }
}


function T1RemoteTurretObj::onDestroyed(%this, %obj, %prevState){
   Parent::onDestroyed(%this, %obj, %prevState);
   $TeamDeployedCount[%obj.team, T1RemoteTurret]--;
   %obj.schedule(500, "delete");
}

datablock AudioProfile(t1RepairPackFireSound)
{
   filename    = "t1sounds/repair.wav";
   description = CloseLooping3d;
   preload = true;
};

//--------------------------------------------------------------------------
// Projectile

datablock RepairProjectileData(DefaultRepairBeam)
{
   sound = RepairPackFireSound;

   beamRange      = 10;
   beamWidth      = 0.15;
   numSegments    = 20;
   texRepeat      = 0.20;
   blurFreq       = 10.0;
   blurLifetime   = 1.0;
   cutoffAngle    = 25.0;
   
   textures[0]   = "special/redbump2";
   textures[1]   = "special/redflare";
};


//-------------------------------------------------------------------------
// shapebase datablocks

datablock ShapeBaseImageData(T1RepairPackImage)
{
   shapeFile = "t1RepairPack.dts";
   item = t1RepairPack;
   mountPoint = 1;
   offset = "0 -0.18 -0.22";
   emap = true;

   gun = t1RepairGunImage;

   stateName[0] = "Idle";
   stateTransitionOnTriggerDown[0] = "Activate";

   stateName[1] = "Activate";
   stateScript[1] = "onActivate";
   stateSequence[1] = "fire";
   stateSound[1] = T1DiscSwitchSound;
   stateTransitionOnTriggerUp[1] = "Deactivate";

   stateName[2] = "Deactivate";
   stateScript[2] = "onDeactivate";
   stateTransitionOnTimeout[2] = "Idle";   
};

datablock ItemData(t1RepairPack)
{
   className = Pack;
   catagory = "Packs";
   shapeFile = "t1RepairPack.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   rotate = true;
   image = "t1RepairPackImage";
   pickUpName = "a repair pack";

   lightOnlyStatic = true;
   lightType = "PulsingLight";
   lightColor = "1 0 0 1";
   lightTime = 1200;
   lightRadius = 4;

   computeCRC = true;
   emap = true;
};

function t1RepairPack::onCollision(%data,%obj,%col){
   if (%col.getDataBlock().className $= Armor && %col.getState() !$= "Dead" && %col.getMountedImage(2) == 0 && !%col.isMounted()){
      if (%col.client){
         messageClient(%col.client, 'MsgItemPickup', '\c0You picked up %1.', %data.pickUpName);
         serverPlay3D(ItemPickupSound, %col.getTransform());
      }
      if (%obj.isStatic()){
         %obj.respawn();
      }
      else{
         %obj.delete();
      }
      %col.setInventory(t1RepairPack, 1, true);
      
   }
}


datablock ShapeBaseImageData(t1RepairGunImage)
{
   shapeFile = "t1RepairPackGun.dts";
   offset = "0 0 0";

   usesEnergy = true;
   minEnergy = 3;
   cutOffEnergy = 3.1;
   emap = true;

   repairFactorPlayer = 0.002; // <--- attention DaveG!
   repairFactorObject = 0.0055; // <--- attention DaveG! // z0dd - ZOD, 7/20/02. was 0.004

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.25;

   stateName[1] = "ActivateReady";
   stateScript[1] = "onActivateReady";
   stateSpinThread[1] = Stop;
   stateTransitionOnAmmo[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "ActivateReady";

   stateName[2] = "Ready";
   stateSpinThread[2] = Stop;
   stateTransitionOnNoAmmo[2] = "Deactivate";
   stateTransitionOnTriggerDown[2] = "Validate";

   stateName[3] = "Validate";
   stateTransitionOnTimeout[3] = "Validate";
   stateTimeoutValue[3] = 0.2;
   stateEnergyDrain[3] = 3;
   stateSpinThread[3] = SpinUp;
   stateScript[3] = "onValidate";
   stateIgnoreLoadedForReady[3] = true;
   stateTransitionOnLoaded[3] = "Repair";
   stateTransitionOnNoAmmo[3] = "Deactivate";
   stateTransitionOnTriggerUp[3] = "Deactivate";

   stateName[4] = "Repair";
   stateSound[4] = t1RepairPackFireSound;
   stateScript[4] = "onRepair";
   stateSpinThread[4] = FullSpeed;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "fire";
   stateFire[4] = true;
   stateEnergyDrain[4] = 9;
   stateTimeoutValue[4] = 0.2;
   stateTransitionOnTimeOut[4] = "Repair";
   stateTransitionOnNoAmmo[4] = "Deactivate";
   stateTransitionOnTriggerUp[4] = "Deactivate";
   stateTransitionOnNotLoaded[4] = "Validate";

   stateName[5] = "Deactivate";
   stateScript[5] = "onDeactivate";
   stateSpinThread[5] = SpinDown;
   stateSequence[5] = "activate";
   stateDirection[5] = false;
   stateTimeoutValue[5] = 0.2;
   stateTransitionOnTimeout[5] = "ActivateReady";
};

function T1RepairPackImage::onUnmount(%data, %obj, %node)
{
   // dismount the repair gun if the player had it mounted
   // need the extra "if" statement to avoid a console error message
   if(%obj.getMountedImage($WeaponSlot))
      if(%obj.getMountedImage($WeaponSlot).getName() $= "t1RepairGunImage")
         %obj.unmountImage($WeaponSlot);

   // if the player was repairing something when the pack was thrown, stop repairing it
   if(%obj.repairing != 0)
      stopRepairing(%obj);
} 

function T1RepairPackImage::onActivate(%data, %obj, %slot)
{
   // don't activate the pack if player is piloting a vehicle
   if(%obj.isPilot() || %obj.getImageState(0) $= "Fire")
   {
      %obj.setImageTrigger(%slot, false);
      return;
   }
   // z0dd - ZOD, 5/19/03. Make sure their wearing a repair pack.
   if(%obj.getMountedImage($BackpackSlot) != 0)
   {
      if(%obj.getMountedImage($BackpackSlot).getName() $= "T1RepairPackImage")
      {
         if(!isObject(%obj.getMountedImage($WeaponSlot)) || %obj.getMountedImage($WeaponSlot).getName() !$= "t1RepairGunImage")
         {
            messageClient(%obj.client, 'MsgRepairPackOn', '\c2Repair pack activated.');

            // make sure player's arm thread is "look"
            %obj.setArmThread(look);

            // mount the repair gun
            %obj.mountImage(t1RepairGunImage, $WeaponSlot);
            // clientCmdsetRepairReticle found in hud.cs
            commandToClient(%obj.client, 'setRepairReticle');
         }
      }
      else
         %obj.setImageTrigger(%slot, false);
   }
}

function T1RepairPackImage::onDeactivate(%data, %obj, %slot)
{
   //called when the player hits the "pack" key again (toggle)
   %obj.setImageTrigger(%slot, false);
   // if repair gun was mounted, unmount it
   if(%obj.getMountedImage($WeaponSlot).getName() $= "t1RepairGunImage")
      %obj.unmountImage($WeaponSlot);
}

function t1RepairGunImage::onMount(%this,%obj,%slot)
{
   %obj.setImageAmmo(%slot,true);
   // z0dd - ZOD, 9/29/02. Removed T2 demo code from here
   commandToClient( %obj.client, 'setRepairPackIconOn' );
}

function t1RepairGunImage::onUnmount(%this,%obj,%slot)
{
   // called when player switches to another weapon

   // stop repairing whatever player was repairing
   if(%obj.repairing)
      stopRepairing(%obj);

   %obj.setImageTrigger(%slot, false);
   // "turn off" the repair pack -- player needs to hit the "pack" key to
   // activate the repair gun again
   %obj.setImageTrigger($BackpackSlot, false);
   // z0dd - ZOD, 9/29/02. Removed T2 demo code from here
   commandToClient( %obj.client, 'setRepairPackIconOff' );
}

function t1RepairGunImage::onActivateReady(%this, %obj, %slot)
{
   %obj.errMsgSent = false;
   %obj.selfRepairing = false;
   %obj.repairing = 0;
   %obj.setImageLoaded(%slot, false);
}

function t1RepairGunImage::onValidate(%this, %obj, %slot)
{
   // this = t1RepairGunImage datablock
   // obj = player wielding the repair gun
   // slot = weapon slot

   if(%obj.getEnergyLevel() <= %this.cutOffEnergy)
   {
      stopRepairing(%obj);
      return;
   }
   // z0dd - ZOD, 5/19/03. Make sure their wearing a repair pack.
   if(%obj.getMountedImage($BackpackSlot) != 0)
   {
      if(%obj.getMountedImage($BackpackSlot).getName() $= "T1RepairPackImage")
      {
         %repGun = %obj.getMountedImage(%slot);
         // muzVec is the vector coming from the repair gun's "muzzle"
         %muzVec = %obj.getMuzzleVector(%slot);
         // muzNVec = normalized muzVec
         %muzNVec = VectorNormalize(%muzVec);
         %repairRange = DefaultRepairBeam.beamRange;
         // scale muzNVec to the range the repair beam can reach
         %muzScaled = VectorScale(%muzNVec, %repairRange);
         // muzPoint = the actual point of the gun's "muzzle"
         %muzPoint = %obj.getMuzzlePoint(%slot);
         // rangeEnd = muzzle point + length of beam
         %rangeEnd = VectorAdd(%muzPoint, %muzScaled);
         // search for just about anything that can be damaged as well as interiors
         %searchMasks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType |
                        $TypeMasks::StaticShapeObjectType | $TypeMasks::TurretObjectType | 
                        $TypeMasks::InteriorObjectType; 

         // search for objects within the beam's range that fit the masks above
         %scanTarg = ContainerRayCast(%muzPoint, %rangeEnd, %searchMasks, %obj);
         // screen out interiors
         if(%scanTarg && !(%scanTarg.getType() & $TypeMasks::InteriorObjectType))
         {
            // a target in range was found
            %repTgt = firstWord(%scanTarg);
            // is the prospective target damaged?
            if(%repTgt.notRepairable)
            {
               // this is an object that cant be repaired at all
               // -- mission specific flag set on the object
               if(!%obj.errMsgSent)
               {
                  messageClient(%obj.client, 'MsgRepairPackIrrepairable', '\c2Target is not repairable.', %repTgt);
                  %obj.errMsgSent = true;
               }
               // if player was repairing something, stop the repairs -- we're done
               if(%obj.repairing)
                  stopRepairing(%obj);
            }
            else if(%repTgt.getDamageLevel())
            {
               // yes, it's damaged
               if(%repTgt != %obj.repairing)
               {
                  if(isObject(%obj.repairing))
                     stopRepairing(%obj);

                  %obj.repairing = %repTgt;
               }
               // setting imageLoaded to true sends us to repair state (function onRepair)
               %obj.setImageLoaded(%slot, true);
            }
            else
            {
               // there is a target in range, but it's not damaged
               if(!%obj.errMsgSent)
               {
                  // if the target isn't damaged, send a message to that effect only once
                  messageClient(%obj.client, 'MsgRepairPackNotDamaged', '\c2Target is not damaged.', %repTgt);
                  %obj.errMsgSent = true;
               }
               // if player was repairing something, stop the repairs -- we're done
               if(%obj.repairing)
               stopRepairing(%obj);
            }
         }
         //AI hack - too many things influence the aiming, so I'm going to force the repair object for bots only
         else if (%obj.client.isAIControlled() && isObject(%obj.client.repairObject))
         {
            %repTgt = %obj.client.repairObject;
            %repPoint = %repTgt.getAIRepairPoint();
            if (%repPoint $= "0 0 0")
               %repPoint = %repTgt.getWorldBoxCenter();

            %repTgtVector = VectorNormalize(VectorSub(%muzPoint, %repPoint));
            %aimVector = VectorNormalize(VectorSub(%muzPoint, %rangeEnd));

            //if the dot product is very close (ie. we're aiming in the right direction)
            if (VectorDot(%repTgtVector, %aimVector) > 0.85)
            {
               //do an LOS to make sure nothing is in the way...
               %scanTarg = ContainerRayCast(%muzPoint, %repPoint, %searchMasks, %obj);
               if (firstWord(%scanTarg) == %repTgt)
               {
                  // yes, it's damaged

                  if(isObject(%obj.repairing))
                     stopRepairing(%obj);

                  %obj.repairing = %repTgt;
                  // setting imageLoaded to true sends us to repair state (function onRepair)
                  %obj.setImageLoaded(%slot, true);
               }
            }
         }
         else if(%obj.getDamageLevel())
         {
            // there is no target in range, but the player is damaged
            // check to see if we were repairing something before -- if so, stop repairing old target
            if(%obj.repairing != 0)
               if(%obj.repairing != %obj)
                  stopRepairing(%obj);

            if(isObject(%obj.repairing))
               stopRepairing(%obj);
      
            %obj.repairing = %obj;
            // quick, to onRepair!
            %obj.setImageLoaded(%slot, true);
         }
         else
         {
            // there is no target in range, and the player isn't damaged
            if(!%obj.errMsgSent)
            {
               // send an error message only once
               messageClient(%obj.client, 'MsgRepairPackNoTarget', '\c2No target to repair.');
               %obj.errMsgSent = true;
            }
            stopRepairing(%obj);
         }
      }
      else
      {
         // z0dd - ZOD, 5/19/03. No repair pack on their back, unmount gun.
         %obj.setImageTrigger(%slot, false);
         %obj.unmountImage(%slot);
      }
   }
   else
   {
      // z0dd - ZOD, 5/19/03. No repair pack on their back, unmount gun.
      %obj.setImageTrigger(%slot, false);
      %obj.unmountImage(%slot);
   }
}

function t1RepairGunImage::onRepair(%this,%obj,%slot)
{
   // this = t1RepairGunImage datablock
   // obj = player wielding the repair gun
   // slot = weapon slot

   if(%obj.getEnergyLevel() <= %this.cutOffEnergy)
   {
      stopRepairing(%obj);
      return;
   }
   // reset the flag that indicates an error message has been sent
   %obj.errMsgSent = false;
   %target = %obj.repairing;
   if(!%target)
   {
      // no target -- whoops! never mind
      stopRepairing(%obj);
   }
   else
   {
      %target.repairedBy = %obj.client;  //keep track of who last repaired this item
      if(%obj.repairing == %obj)
      {
         // player is self-repairing
         if(%obj.getDamageLevel())
         {
            if(!%obj.selfRepairing)
            {
               // no need for a projectile, just send a message and up the repair rate
               messageClient(%obj.client, 'MsgRepairPackPlayerSelfRepair', '\c2Repairing self.');
               %obj.selfRepairing = true;
               startRepairing(%obj, true);
            }
         }
         else
         {
            messageClient(%obj.client, 'MsgRepairPackSelfDone', '\c2Repairs completed on self.');
            stopRepairing(%obj);
            %obj.errMsgSent = true;
         }
      }
      else
      {
         // make sure we still have a target -- more vector fun!!!
         %muzVec      = %obj.getMuzzleVector(%slot);
         %muzNVec     = VectorNormalize(%muzVec);
         %repairRange = DefaultRepairBeam.beamRange;
         %muzScaled   = VectorScale(%muzNVec, %repairRange);
         %muzPoint    = %obj.getMuzzlePoint(%slot);
         %rangeEnd    = VectorAdd(%muzPoint, %muzScaled);

         %searchMasks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | 
                        $TypeMasks::StaticShapeObjectType | $TypeMasks::TurretObjectType;

         //AI hack to help "fudge" the repairing stuff...
         if (%obj.client.isAIControlled() && isObject(%obj.client.repairObject) && %obj.client.repairObject == %obj.repairing)
         {
            %repTgt = %obj.client.repairObject;
            %repPoint = %repTgt.getAIRepairPoint();
            if (%repPoint $= "0 0 0")
               %repPoint = %repTgt.getWorldBoxCenter();
            %repTgtVector = VectorNormalize(VectorSub(%muzPoint, %repPoint));
            %aimVector = VectorNormalize(VectorSub(%muzPoint, %rangeEnd));
 
            //if the dot product is very close (ie. we're aiming in the right direction)
            if (VectorDot(%repTgtVector, %aimVector) > 0.85)
               %scanTarg = ContainerRayCast(%muzPoint, %repPoint, %searchMasks, %obj);
         }
         else
            %scanTarg = ContainerRayCast(%muzPoint, %rangeEnd, %searchMasks, %obj);

         if (%scanTarg)
         {
            %pos = getWords(%scanTarg, 1, 3);
            %obstructMask = $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType;
            %obstruction = ContainerRayCast(%muzPoint, %pos, %obstructMask, %obj);
            if (%obstruction)
               %scanTarg = "0";
         }

         if(%scanTarg)
         {
            // there's still a target out there
            %repTgt = firstWord(%scanTarg);
            // is the target damaged?
            if(%repTgt.getDamageLevel())
            {
               if(%repTgt != %obj.repairing)
               {
                  // the target is not the same as the one we were just repairing
                  // stop repairing old target, start repairing new target
                  stopRepairing(%obj);
                  if(isObject(%obj.repairing))
                     stopRepairing(%obj);
 
                  %obj.repairing = %repTgt;
                  // extract the name of what player is repairing based on what it is
                  // if it's a player, it's the player's name (duh)
                  // if it's an object, look for a nametag
                  // if object has no nametag, just say what it is (e.g. generatorLarge)
                  if(%repTgt.getClassName() $= Player)
                     %tgtName = getTaggedString(%repTgt.client.name);
                  else if(%repTgt.getGameName() !$= "")
                     %tgtName = %repTgt.getGameName();
                  else
                     %tgtName = %repTgt.getDatablock().getName();
                  messageClient(%obj.client, 'MsgRepairPackRepairingObj', '\c2Repairing %1.', %tgtName, %repTgt);
                  startRepairing(%obj, false);
               }
               else
               {
                  // it's the same target as last time
                  // changed to fix "2 players can't repair same object" bug
                  if(%obj.repairProjectile == 0)
                  {
                     if(%repTgt.getClassName() $= Player)
                        %tgtName = getTaggedString(%repTgt.client.name);
                     else if(%repTgt.getGameName() !$= "")
                        %tgtName = %repTgt.getGameName();
                     else
                        %tgtName = %repTgt.getDatablock().getName();
                     messageClient(%obj.client, 'MsgRepairPackRepairingObj', '\c2Repairing %1.', %tgtName, %repTgt);
                     startRepairing(%obj, false);
                  }
               }
               // z0dd - ZOD, 8/9/03. It's enabled, award points
               //if(%repTgt.getDamageState() $= "Enabled")
               //   Game.objectRepaired(%repTgt, %tgtName);
            }
            else
            {
               %rateOfRepair = %this.repairFactorObject;
               if(%repTgt.getClassName() $= Player)
               {
                  %tgtName = getTaggedString(%repTgt.client.name);
                  %rateOfRepair = %this.repairFactorPlayer;
               }
               else if(%repTgt.getGameName() !$= "")
                  %tgtName = %repTgt.getGameName();
               else
                  %tgtName = %repTgt.getDatablock().getName();
               if(%repTgt != %obj.repairing)
               {
                  // it isn't the same object we were repairing previously
                  messageClient(%obj.client, 'MsgRepairPackNotDamaged', '\c2%1 is not damaged.', %tgtName);
               }
               else
               {
                  // same target, but not damaged -- we must be done
                  messageClient(%obj.client, 'MsgRepairPackDone', '\c2Repairs completed.');
                  // z0dd - ZOD, 8/9/03. Award for enabling item, so this is too late
                  Game.objectRepaired(%repTgt, %tgtName);
               }
               %obj.errMsgSent = true;
               stopRepairing(%obj);
            }
         }
         else
         {
            // whoops, we lost our target
            messageClient(%obj.client, 'MsgRepairPackLostTarget', '\c2Repair target no longer in range.');
            stopRepairing(%obj);
         }
      }
   }
}

function t1RepairGunImage::onDeactivate(%this,%obj,%slot)
{
   stopRepairing(%obj);
}

function stopRepairing(%player)
{
   // %player = the player who was using the repair pack

   if(%player.selfRepairing)
   {
      // there is no projectile for self-repairing
      %player.setRepairRate(%player.getRepairRate() - %player.repairingRate);
      %player.selfRepairing = false;
   }
   else if(%player.repairing > 0)
   {
      // player was repairing something else
      //if(%player.repairing.beingRepaired > 0)
      //{
         // don't decrement this stuff if it's already at 0 -- though it shouldn't be
         //%player.repairing.beingRepaired--;
         %player.repairing.setRepairRate(%player.repairing.getRepairRate() - %player.repairingRate);
      //}
      if(%player.repairProjectile > 0)
      {
         // is there a repair projectile? delete it
         %player.repairProjectile.delete();
         %player.repairProjectile = 0;
      }
   }
   %player.repairing = 0;
   %player.repairingRate = 0;
   %player.setImageTrigger($WeaponSlot, false);
   %player.setImageLoaded($WeaponSlot, false);
}

function startRepairing(%player, %self)
{
   // %player = the player who was using the repair pack
   // %self = boolean -- is player repairing him/herself?

   if(%self)
   {
      // one repair, hold the projectile
      %player.setRepairRate(%player.getRepairRate() + t1RepairGunImage.repairFactorPlayer);
      %player.selfRepairing = true;
      %player.repairingRate = t1RepairGunImage.repairFactorPlayer;
   }
   else
   {
      //if(%player.repairing.beingRepaired $= "")
      //   %player.repairing.beingRepaired = 1;
      //else
      //   %player.repairing.beingRepaired++;

      //AI hack...
      if (%player.client.isAIControlled() && %player.client.repairObject == %player.repairing)
      {
         %initialPosition  = %player.getMuzzlePoint($WeaponSlot);
         %initialDirection = VectorSub(%initialPosition, %player.repairing.getWorldBoxCenter());
      }
      else
      {
         %initialDirection = %player.getMuzzleVector($WeaponSlot);
         %initialPosition  = %player.getMuzzlePoint($WeaponSlot);
      }
      if(%player.repairing.getClassName() $= Player)
         %repRate = t1RepairGunImage.repairFactorPlayer;
      else
         %repRate = t1RepairGunImage.repairFactorObject;
      %player.repairing.setRepairRate(%player.repairing.getRepairRate() + %repRate);
 
      %player.repairingRate = %repRate;
      %player.repairProjectile = new RepairProjectile() {
         dataBlock = DefaultRepairBeam;
         initialDirection = %initialDirection;
         initialPosition  = %initialPosition;
         sourceObject     = %player;
         sourceSlot       = $WeaponSlot;
         targetObject     = %player.repairing;
      };
      // ----------------------------------------------------
      // z0dd - ZOD, 5/27/02. Fix lingering projectile bug
      if(isObject(%player.lastProjectile))
         %player.lastProjectile.delete();

      %player.lastProjectile = %player.repairProjectile;
      // End z0dd - ZOD 
      // ----------------------------------------------------
      MissionCleanup.add(%player.repairProjectile);
   }
}

function serverCmdClientPickVeh(%client, %callName, %index){
   call(%callName, %client, %index);   
}


function buyt1Veh(%client, %index){
   %station = %client.player.atVPad;
   %vpad = %station.vPadObj.getID(); 
   if(!%station.isPowered()){
      messageClient(%client, 'msgStationDenied', '\c2Station has no power. ~wt1sounds/Access_Denied.wav'); 
      return;
   }
   if(%station.isDisabled() || %station.isDestroyed()){
      messageClient(%client, 'msgStationDenied', '\c2Station is disabled.~wt1sounds/Access_Denied.wav'); 
      return;
   }
   if(%vpad.isDisabled() ||  %vpad.isDestroyed()){
      messageClient(%client, 'msgStationDenied', '\c2Vehicle pad is disabled.~wt1sounds/Access_Denied.wav');
      return;
   }
   switch(%index){
      case 2: %db = T1ScoutFlyer;
      case 3: %db = T1LAPCFlyer;
      case 4: %db = T1HAPCFlyer;
      default: return;
   }
   if(vehicleCheck(%db, %client.team)){
      if(isObject(%station) && isObject(%vpad)){
         if(!%station.isNotReady){
            %mask = $TypeMasks::VehicleObjectType | $TypeMasks::PlayerObjectType | 
                  $TypeMasks::StationObjectType | $TypeMasks::TurretObjectType;
            InitContainerRadiusSearch(vectorAdd(%vpad.getPosition(),"0 0 5"), 10, %mask);
            %clear = 1;
            for (%x = 0; (%obj = containerSearchNext()) != 0; %x++){

               if(%obj != %vpad && %obj != %station){ 
                  if((%obj.getType() & $TypeMasks::VehicleObjectType) && (%obj.getDataBlock().checkIfPlayersMounted(%obj))){
                     %clear = 0;
                     break;
                  }
                  else{
                     %removeObjects[%x] = %obj;

                  }
               }
            }
            if(%clear) {
               %fadeTime = 0;
               for(%i = 0; %i < %x; %i++){
                  if(isObject(%removeObjects[%i])){
                     if(%removeObjects[%i].getType() & $TypeMasks::PlayerObjectType) {
                        %pData = %removeObjects[%i].getDataBlock();
                        %pData.damageObject(%removeObjects[%i], 0, "0 0 0", 1000, $DamageType::VehicleSpawn);
                     }
                     else{
                        %removeObjects[%i].mountable = 0;
                        %removeObjects[%i].startFade( 1000, 0, true );
                        %removeObjects[%i].schedule(1001, "delete");
                        %fadeTime = 1500;
                     }
                  }
               }
               %station.isNotReady = 1;
               schedule(%fadeTime, 0, "makeT1Veh", %client, %db, %station, %vpad);
            }
            else{
               MessageClient(%client, "", 'Can\'t create vehicle. A player is on the creation pad.'); 
            }
         }
         else{
            messageClient(%client, 'msgStationDenied', '\c2Station not ready yet please wait.~wt1sounds/Access_Denied.wav');
         }
      }
      else{
         messageClient(%client, 'msgStationDenied', '\c2Station Obj not found.~wt1sounds/Access_Denied.wav');  
      }
   }
   else{
      messageClient(%client, 'msgStationDenied', '\c2Team limit reached ~wt1sounds/Access_Denied.wav');  
   }
}

function makeT1Veh(%client, %db, %station, %vpad){
   %flyObj = new FlyingVehicle() {
      position = vectorAdd(%vpad.getPosition(), "0 0 4");
      rotation = %vpad.rotation;
      scale = "1 1 1";
      dataBlock = %db;
      team = %client.team;
   };
   MissionCleanup.add(%flyObj);
   vehicleListAdd(%db, %flyObj);
   if(%client.player.lastVehicle){
         %client.player.lastVehicle.lastPilot = "";
         vehicleAbandonTimeOut(%client.player.lastVehicle);
         %client.player.lastVehicle = "";
   }   
   %client.player.lastVehicle = %flyObj;
   %flyObj.lastPilot = %client.player;
   if(%flyObj.getTarget() != -1)
      setTargetSensorGroup(%flyObj.getTarget(), %client.getSensorGroup());
   %flyObj.setCloaked(true);
   %flyObj.schedule(140, "playAudio", 0, VehicleAppearSound);
   %flyObj.schedule(1200, "setCloaked", false);
   commandToClient(%client,'PickVehMenu', 0); 
   if ( %client.isVehicleTeleportEnabled() )
         %flyObj.getDataBlock().schedule(1500, "mountDriver", %flyObj, %client.player);
   schedule(6000, 0, "vpadLockOut", %station);
}

function vpadLockOut(%obj){
   %obj.isNotReady = 0;  
}

function CTFGame::equip(%game, %player){
   if(isObject(StarsiegeTribesMap) && (StarsiegeTribesMap.teamOnly == %player.team || StarsiegeTribesMap.teamOnly == 0)){
      for(%i =0; %i<$InventoryHudCount; %i++)
         %player.client.setInventoryHudItem($InventoryHudData[%i, itemDataName], 0, 1);
      %player.client.clearBackpackIcon(); 
      //%player.setArmor("Light");
      %player.setInventory(RepairKit,1);
      %player.setInventory(T1Grenade,6);
      %player.setInventory(T1Blaster,1);
      %player.setInventory(T1Disc,1);
      %player.setInventory(T1ChainGun, 1);
      %player.setInventory(T1ChainGunAmmo, 100);
      %player.setInventory(T1DiscAmmo, 20);
      %player.setInventory(T1Beacon, 3);
      %player.setInventory(TargetingLaser, 1);
      %player.weaponCount = 3;
      
      %player.use("T1Blaster");

   }
   else{
      parent::equip(%game, %player);
   }
}  

$t1wepSub["TargetingLaser"] = "T1TargetingLaser";
$t1wepSub["ELFGun"] = "T1ELF";
$t1wepSub["SniperRifle"] = "T1Sniper";
$t1wepSub["Blaster"] = "T1Blaster";

$t1wepSub["Disc"] = "T1Disc";
$t1wepSub["Plasma"] = "T1Plasma";
$t1wepSub["Mortar"] = "T1Mortar";
$t1wepSub["GrenadeLauncher"] = "T1GL";
$t1wepSub["Chaingun"] = "T1ChainGun";

$t1AmmoWep[0] = "T1Disc";
$t1AmmoWep[1] = "T1Plasma";
$t1AmmoWep[2] = "T1Mortar";
$t1AmmoWep[3] = "T1GL";
$t1AmmoWep[4] = "T1ChainGun";


$t1AmmoWepEQ[0] = "Disc";
$t1AmmoWepEQ[1] = "Plasma";
$t1AmmoWepEQ[2] = "Mortar";
$t1AmmoWepEQ[3] = "GrenadeLauncher";
$t1AmmoWepEQ[4] = "Chaingun";

$t1EngWep[0] = "T1TargetingLaser";
$t1EngWep[1] = "T1ELF";
$t1EngWep[2] = "T1Sniper";
$t1EngWep[3] = "T1Blaster";

$t1EngWepEQ[0] = "TargetingLaser";
$t1EngWepEQ[1] = "ELFGun";
$t1EngWepEQ[2] = "SniperRifle";
$t1EngWepEQ[3] = "Blaster";

$armorArray[0] = LightMaleHumanArmor;
$armorArray[1] = MediumMaleHumanArmor;
$armorArray[2] = HeavyMaleHumanArmor;
$armorArray[3] = LightFemaleHumanArmor;
$armorArray[4] =  MediumFemaleHumanArmor;
$armorArray[5] = HeavyFemaleHumanArmor;
$armorArray[6] = LightMaleBiodermArmor;
$armorArray[7] = MediumMaleBiodermArmor;
$armorArray[8] = HeavyMaleBiodermArmor;
// The retro armors need the same T1 max[] limits as everything else, or a retro
// player cannot carry a single T1 weapon.  They inherit from the stock armors, but
// inheritance copies at DEFINITION time and loadRetInfo(1) does not run until
// T1StartObj::onAdd -- long after -- so the copy would always be of the unpatched
// values.  Patch them directly instead.  (See the armor block at the end of this file.)
$armorArray[9]  = LightMaleTR1Armor;
$armorArray[10] = MediumMaleTR1Armor;
$armorArray[11] = HeavyMaleTR1Armor;
$armorArray[12] = LightFemaleTR1Armor;
$armorArray[13] = MediumFemaleTR1Armor;
$armorArray[14] = HeavyFemaleTR1Armor;



function t1ammoarray(){
   if(!$t1SetupAmmoArray){
      $t1SetupAmmoArray = 1;
      
      for(%i = 0; %i < 100; %i++){// find the end of the array
         if($ammoType[%i] $= ""){
            break;
         }
      }
      $ammoType[%i] = "T1DiscAmmo";
      %i++;
      $ammoType[%i] = "T1PlasmaAmmo";
      %i++;
      $ammoType[%i] = "T1MortarAmmo";
      %i++;
      $ammoType[%i] = "T1GLAmmo";
      %i++;
      $ammoType[%i] = "T1ChainGunAmmo";
      %i++;
   }
}t1ammoarray();

function loadRetInfo(%bypass){
   error("loadRetInfo bypass: " @ %bypass);
   if(!$t1loadRet || %bypass){ 
      $t1loadRet = 1;

      $WeaponsHudData[$WeaponsHudCount, bitmapName] = "gui/hud_disc";
      $WeaponsHudData[$WeaponsHudCount, itemDataName] = "T1Disc";
      $WeaponsHudData[$WeaponsHudCount, ammoDataName] = "T1DiscAmmo";
      $WeaponsHudData[$WeaponsHudCount, reticle] = "gui/ret_disc";
      $WeaponsHudData[$WeaponsHudCount, visible] = "true";
      $WeaponsHudCount++;

      $WeaponsHudData[$WeaponsHudCount, bitmapName] = "gui/hud_plasma";
      $WeaponsHudData[$WeaponsHudCount, itemDataName] = "T1Plasma";
      $WeaponsHudData[$WeaponsHudCount, ammoDataName] = "T1PlasmaAmmo";
      $WeaponsHudData[$WeaponsHudCount, reticle] = "gui/ret_plasma";
      $WeaponsHudData[$WeaponsHudCount, visible] = "true";
      $WeaponsHudCount++;
      $WeaponsHudData[$WeaponsHudCount, bitmapName] = "gui/hud_mortor";
      $WeaponsHudData[$WeaponsHudCount, itemDataName] = "T1Mortar";
      $WeaponsHudData[$WeaponsHudCount, ammoDataName] = "T1MortarAmmo";
      $WeaponsHudData[$WeaponsHudCount, reticle] = "gui/ret_mortor";
      $WeaponsHudData[$WeaponsHudCount, visible] = "true";
      $WeaponsHudCount++;
      $WeaponsHudData[$WeaponsHudCount, bitmapName] = "gui/hud_grenlaunch";
      $WeaponsHudData[$WeaponsHudCount, itemDataName] = "T1GL";
      $WeaponsHudData[$WeaponsHudCount, ammoDataName] = "T1GrenadeLauncherAmmo";
      $WeaponsHudData[$WeaponsHudCount, reticle] = "gui/ret_grenade";
      $WeaponsHudData[$WeaponsHudCount, visible] = "true";
      $WeaponsHudCount++;
      $WeaponsHudData[$WeaponsHudCount, bitmapName] = "gui/hud_chaingun";
      $WeaponsHudData[$WeaponsHudCount, itemDataName] = "T1Chaingun";
      $WeaponsHudData[$WeaponsHudCount, ammoDataName] = "T1ChaingunAmmo";
      $WeaponsHudData[$WeaponsHudCount, reticle] = "gui/ret_chaingun";
      $WeaponsHudData[$WeaponsHudCount, visible] = "true";
      $WeaponsHudCount++;
      $WeaponsHudData[$WeaponsHudCount, bitmapName] = "gui/hud_sniper";
      $WeaponsHudData[$WeaponsHudCount, itemDataName] = "T1Sniper";
      $WeaponsHudData[$WeaponsHudCount, reticle] = "gui/hud_ret_sniper";
      $WeaponsHudData[$WeaponsHudCount, visible] = "false";
      $WeaponsHudCount++;
      $WeaponsHudData[$WeaponsHudCount, bitmapName] = "gui/hud_elfgun";
      $WeaponsHudData[$WeaponsHudCount, itemDataName] = "T1ELF";
      $WeaponsHudData[$WeaponsHudCount, reticle] = "gui/ret_elf";
      $WeaponsHudData[$WeaponsHudCount, visible] = "true";
      $WeaponsHudCount++;
      $WeaponsHudData[$WeaponsHudCount, bitmapName] = "gui/hud_blaster";
      $WeaponsHudData[$WeaponsHudCount, itemDataName] = "T1Blaster";
      $WeaponsHudData[$WeaponsHudCount, reticle] = "gui/ret_blaster";
      $WeaponsHudData[$WeaponsHudCount, visible] = "true";
      $WeaponsHudCount++;
      $WeaponsHudData[$WeaponsHudCount, bitmapName]   = "gui/hud_targetlaser";
      $WeaponsHudData[$WeaponsHudCount, itemDataName] = "T1TargetingLaser";
      $WeaponsHudData[$WeaponsHudCount, reticle] = "gui/hud_ret_targlaser";
      $WeaponsHudData[$WeaponsHudCount, visible] = "false";
      $WeaponsHudCount++;
 
      $BackpackHudData[$BackpackHudCount, itemDataName] = "t1RepairPack";
      $BackpackHudData[$BackpackHudCount, bitmapName] = "gui/hud_new_packrepair";
      $BackpackHudCount++;

      $BackpackHudData[$BackpackHudCount, itemDataName] = "T1AmmoDeployable";
      $BackpackHudData[$BackpackHudCount, bitmapName] = "gui/hud_new_packinventory";
      $BackpackHudCount++;

      $BackpackHudData[$BackpackHudCount, itemDataName] = "T1RemoteTurret";
      $BackpackHudData[$BackpackHudCount, bitmapName] = "gui/hud_new_packturretout";
      $BackpackHudCount++;

      $BackpackHudData[$BackpackHudCount, itemDataName] = "T1InvyDeployable";
      $BackpackHudData[$BackpackHudCount, bitmapName] = "gui/hud_new_packinventory";
      $BackpackHudCount++;  

      $BackpackHudData[$BackpackHudCount, itemDataName] = "T1AmmoPack";
      $BackpackHudData[$BackpackHudCount, bitmapName] = "gui/hud_new_packammo";
      $BackpackHudCount++; 

      $BackpackHudData[$BackpackHudCount, itemDataName] = "T1EnergyPack";
      $BackpackHudData[$BackpackHudCount, bitmapName] = "gui/hud_new_packenergy";
      $BackpackHudCount++; 

      $BackpackHudData[$BackpackHudCount, itemDataName] = "T1ShieldPack";
      $BackpackHudData[$BackpackHudCount, bitmapName] = "gui/hud_new_packshield";
      $BackpackHudCount++; 

      $BackpackHudData[$BackpackHudCount, itemDataName] = "T1SensorJammerPack";
      $BackpackHudData[$BackpackHudCount, bitmapName] = "gui/hud_new_packsensjam";
      $BackpackHudCount++; 

      $BackpackHudData[$BackpackHudCount, itemDataName] = "T1SensorJammerPack";
      $BackpackHudData[$BackpackHudCount, bitmapName] = "gui/hud_new_packsensjam";
      $BackpackHudCount++; 

      $BackpackHudData[$BackpackHudCount, itemDataName] = "T1MotionSensorDeployable";
      $BackpackHudData[$BackpackHudCount, bitmapName] = "gui/hud_new_packmotionsens";
      $BackpackHudCount++; 

      $BackpackHudData[$BackpackHudCount, itemDataName] = "T1PulseSensorDeployable";
      $BackpackHudData[$BackpackHudCount, bitmapName] = "gui/hud_new_packradar";
      $BackpackHudCount++; 

      $InventoryHudData[$InventoryHudCount, bitmapName]   = "gui/hud_handgren";
      $InventoryHudData[$InventoryHudCount, itemDataName] = T1Grenade;
      $InventoryHudData[$InventoryHudCount, ammoDataName] = T1Grenade;
      $InventoryHudData[$InventoryHudCount, slot]         = 0;
      $InventoryHudCount++;

      $InventoryHudData[$InventoryHudCount, bitmapName]   = "gui/hud_mine";
      $InventoryHudData[$InventoryHudCount, itemDataName] = T1Mine;
      $InventoryHudData[$InventoryHudCount, ammoDataName] = T1Mine;
      $InventoryHudData[$InventoryHudCount, slot]         = 1;
      $InventoryHudCount++;

      $InventoryHudData[$InventoryHudCount, bitmapName]   = "gui/hud_beacon";
      $InventoryHudData[$InventoryHudCount, itemDataName] = T1Beacon;
      $InventoryHudData[$InventoryHudCount, ammoDataName] = T1Beacon;
      $InventoryHudData[$InventoryHudCount, slot]         = 2;
      $InventoryHudCount++;

      // Slot 0 and the hand-grenade bitmap, matching retail's own camera grenade
      // (hud.cs:488-491) -- it is a grenade as far as the HUD is concerned.
      $InventoryHudData[$InventoryHudCount, bitmapName]   = "gui/hud_handgren";
      $InventoryHudData[$InventoryHudCount, itemDataName] = T1CameraGrenade;
      $InventoryHudData[$InventoryHudCount, ammoDataName] = T1CameraGrenade;
      $InventoryHudData[$InventoryHudCount, slot]         = 0;
      $InventoryHudCount++;


      for(%a = 0; $armorArray[%a] !$= ""; %a++){
         %armor = $armorArray[%a];
         if(!isObject(%armor))// retro armors are absent until the block at the end of
            continue;         // this file has run, and on a non-retro build never exist
         for(%i = 0; %i < 5; %i++){
            %armor.max[$t1AmmoWep[%i]] = %armor.max[$t1AmmoWepEQ[%i]];
            %armor.max[$t1AmmoWep[%i].image.ammo] = %armor.max[$t1AmmoWepEQ[%i].image.ammo];
         }
         for(%i = 0; %i < 4; %i++){
            %armor.max[$t1EngWep[%i]] = %armor.max[$t1EngWepEQ[%i]];
         }
         %armor.max[T1Mine]  = %armor.max[Mine];//copy mine values from 
         %armor.max[T1Grenade]  = %armor.max[Grenade];
         %armor.max[T1AmmoPack]  = %armor.max[AmmoPack];
         %armor.max[T1EnergyPack]  = %armor.max[EnergyPack];
         %armor.max[T1ShieldPack]  = %armor.max[ShieldPack];
         %armor.max[T1SensorJammerPack]  = %armor.max[SensorJammerPack];
         %armor.max[T1PulseSensorDeployable]  = %armor.max[PulseSensorDeployable];
         %armor.max[T1MotionSensorDeployable]  = %armor.max[MotionSensorDeployable];
         %armor.max[T1Beacon]  = %armor.max[Beacon];
         %armor.max[T1CameraGrenade]  = %armor.max[CameraGrenade];
      }
      lightMaleHumanArmor.starsiegeData = true;
   }
}loadRetInfo();


function t1buyFavorites(%client)
{
   if(isObject(Game)) // z0dd - ZOD, 8/9/03. No armors in Spawn CTF.
   {
      if(Game.class $= SCtFGame)
      {
         buyDeployableFavorites(%client);
         return;
      }
   }
   // z0dd - ZOD, 5/27/03. Check to see if we reached the cap on armors, if so, buy ammo and go away mad.
   if(%client.favorites[0] !$= "Scout" && !$Host::TournamentMode && $LimitArmors)
   {
      if($TeamArmorCount[%client.team, $NameToInv[%client.favorites[0]]] >= $TeamArmorMax)
      {
         messageClient(%client, 'MsgTeamDepObjCount', '\c2Your team has reached the maximum (%2) allotment of %1 armors', %client.favorites[0], $TeamArmorMax);
         T1getAmmoStationLovin(%client);
         return;
      }
   }

   // z0dd - ZOD, 5/27/03. Increase the teams armor count and let the player know whats left etc.
   if(!$Host::TournamentMode && $LimitArmors)
   {
      $TeamArmorCount[%client.team, %client.armor]--;
      $TeamArmorCount[%client.team, $NameToInv[%client.favorites[0]]]++;
      if(%client.favorites[0] !$= "Scout")
         messageClient(%client, 'MsgTeamDepObjCount', '\c2Your team has %1 of %2 %3 armors in use', $TeamArmorCount[%client.team, $NameToInv[%client.favorites[0]]], $TeamArmorMax, %client.favorites[0]);
   }

   // don't forget -- for many functions, anything done here also needs to be done
   // below in buyDeployableFavorites !!!
   %client.player.t1clearInventory();
   %client.setWeaponsHudClearAll();
   %cmt = $CurrentMissionType;
   %curArmor = %client.player.getDatablock();
   %curDmgPct = getDamagePercent(%curArmor.maxDamage, %client.player.getDamageLevel());

   // armor
   %client.armor = $NameToInv[%client.favorites[0]];
   %client.player.setArmor( %client.armor );
   %newArmor = %client.player.getDataBlock();
   %client.player.setDamageLevel(%curDmgPct * %newArmor.maxDamage);
   %client.player.setEnergyLevel( %client.player.getDataBlock().maxEnergy);
   %weaponCount = 0;

   // weapons
   for(%i = 0; %i < getFieldCount( %client.weaponIndex ); %i++)
   {
      %inv = $NameToInv[%client.favorites[getField( %client.weaponIndex, %i )]];
      if(!($InvBanList[%cmt, %inv])){
         if( %inv !$= "" )
         {   
            %inv = $t1wepSub[%inv] !$= "" ? $t1wepSub[%inv] : %inv;
            %weaponCount++;
            %client.player.setInventory( %inv, 1 );
         }
         
         // ----------------------------------------------------
         // z0dd - ZOD, 4/24/02. Code optimization.
         if ( %inv.image.ammo !$= "" )
            %client.player.setInventory( %inv.image.ammo, 999 );
         // ----------------------------------------------------
      }
   }
   %client.player.weaponCount = %weaponCount;

   // pack
   %pCh = $NameToInv[%client.favorites[%client.packIndex]];
   if(!($InvBanList[%cmt, %pCh])){
      if ( %pCh $= "" )
         %client.clearBackpackIcon();
      else{
         switch$(%pch){
            case "RepairPack":
               %client.player.setInventory( "t1RepairPack", 1, 1);
            case "AmmoPack":
               %client.player.setInventory( "T1AmmoPack", 1, 1);
            case "EnergyPack":
               %client.player.setInventory( "T1EnergyPack", 1, 1);
            case "ShieldPack":
               %client.player.setInventory( "T1ShieldPack", 1, 1);
            case "SensorJammerPack":
               %client.player.setInventory( "T1SensorJammerPack", 1, 1);
            case "InventoryDeployable":
               %client.player.setInventory( "T1InvyDeployable", 1, 1);
            case "TurretIndoorDeployable" or "TurretOutdoorDeployable":
               %client.player.setInventory( "T1RemoteTurret", 1, 1);
            case "PulseSensorDeployable":
               %client.player.setInventory( "T1PulseSensorDeployable", 2, 1);
            case "MotionSensorDeployable":
               %client.player.setInventory( "T1MotionSensorDeployable", 2, 1);
            default:
               %client.player.setInventory( %pCh, 1 );
         }
      }
   }

   // if this pack is a deployable that has a team limit, warn the purchaser
	// if it's a deployable turret, the limit depends on the number of players (deployables.cs)
	if(%pCh $= "TurretIndoorDeployable" || %pCh $= "TurretOutdoorDeployable")
		%maxDep = countTurretsAllowed(%pCh);
	else
	   %maxDep = $TeamDeployableMax[%pCh];

   if(%maxDep !$= "")
   {
      %depSoFar = $TeamDeployedCount[%client.player.team, %pCh];
      %packName = %client.favorites[%client.packIndex];

      if(Game.numTeams > 1)
         %msTxt = "Your team has "@%depSoFar@" of "@%maxDep SPC %packName@"s deployed.";
      else
         %msTxt = "You have deployed "@%depSoFar@" of "@%maxDep SPC %packName@"s.";

      messageClient(%client, 'MsgTeamDepObjCount', %msTxt);
   }

   // grenades
   for ( %i = 0; %i < getFieldCount( %client.grenadeIndex ); %i++ )
   {
      %data = $NameToInv[%client.favorites[getField( %client.grenadeIndex, %i )]];
      if ( !($InvBanList[%cmt, %data]) ){
         %data = (%data $= "Grenade") ? "T1Grenade" : %data;
         // "Deployable Camera" is $InvGrenade[4], so it arrives through this same loop
         %data = (%data $= "CameraGrenade") ? "T1CameraGrenade" : %data;
                error("t1buyFavorites: grenadeIndex[" @ %i @ "] = " @ %data);
        %client.player.setInventory(%data, 30 );
      }
   }

   %client.player.lastGrenade = $NameToInv[%client.favorites[getField( %client.grenadeIndex,%i )]];

   // if player is buying cameras, show how many are already deployed
   if(%client.favorites[%client.grenadeIndex] $= "Deployable Camera")
   {
      %maxDep = $TeamDeployableMax[DeployedCamera];
      %depSoFar = $TeamDeployedCount[%client.player.team, DeployedCamera];
      if(Game.numTeams > 1)
         %msTxt = "Your team has "@%depSoFar@" of "@%maxDep@" Deployable Cameras placed.";
      else
         %msTxt = "You have placed "@%depSoFar@" of "@%maxDep@" Deployable Cameras.";
      messageClient(%client, 'MsgTeamDepObjCount', %msTxt);
   }

   // mines
   // -----------------------------------------------------------------------------------------------------
   // z0dd - ZOD, 4/24/02. Old code did not check to see if mines are banned, fixed.
   for ( %i = 0; %i < getFieldCount( %client.mineIndex ); %i++ )
   {
      if ( !($InvBanList[%cmt, $NameToInv[%client.favorites[getField( %client.mineIndex, %i )]]]) )
        %client.player.setInventory(T1Mine, 30);
   }
   // -----------------------------------------------------------------------------------------------------
   // miscellaneous stuff -- Repair Kit, Beacons, Targeting Laser
   if ( !($InvBanList[%cmt, RepairKit]) )
      %client.player.setInventory( RepairKit, 1 );
   if ( !($InvBanList[%cmt, Beacon]) )
      %client.player.setInventory( T1Beacon, 20 ); // z0dd - ZOD, 4/24/02. 400 was a bit much, changed to 20
   if ( !($InvBanList[%cmt, TargetingLaser]) )
      %client.player.setInventory( TargetingLaser, 1 );

   // ammo pack pass -- hack! hack!
   if( %pCh $= "AmmoPack" )
      t1InvAmmoPackPass(%client);
}


function t1buyDeployableFavorites(%client)
{
   %player = %client.player;
	%prevPack = %player.getMountedImage($BackpackSlot);
   %player.t1clearInventory();
   %client.setWeaponsHudClearAll();
   %cmt = $CurrentMissionType;

   // players cannot buy armor from deployable inventory stations
	%weapCount = 0;
   for ( %i = 0; %i < getFieldCount( %client.weaponIndex ); %i++ )
   {
      %inv = $NameToInv[%client.favorites[getField( %client.weaponIndex, %i )]];
      if ( !($InvBanList[DeployInv, %inv]) &&  !$InvBanList[%cmt, %inv])
      {
         %inv = $t1wepSub[%inv] !$= "" ? $t1wepSub[%inv] : %inv;
         %player.setInventory( %inv, 1 );
			// increment weapon count if current armor can hold this weapon
         if(%player.getDatablock().max[%inv] > 0)      
				%weapCount++;
      // ---------------------------------------------
      // z0dd - ZOD, 4/24/02. Code streamlining.
      if ( %inv.image.ammo !$= "" )
         %player.setInventory( %inv.image.ammo, 999 );
      // ---------------------------------------------
	if(%weapCount >= %player.getDatablock().maxWeapons)
		break;
      }
   }
   %player.weaponCount = %weapCount;
   // give player the grenades and mines they chose, beacons, and a repair kit
   for ( %i = 0; %i < getFieldCount( %client.grenadeIndex ); %i++)
   {   
      %GInv = $NameToInv[%client.favorites[getField( %client.grenadeIndex, %i )]];
      %client.player.lastGrenade = %GInv;
      if ( !($InvBanList[DeployInv, %GInv])  &&  !$InvBanList[%cmt, %GInv]){
         %GInv = (%GInv $= "Grenade") ? "T1Grenade" : %GInv;
         %GInv = (%GInv $= "Mine") ? "T1Mine" : %GInv;
         %GInv = (%GInv $= "CameraGrenade") ? "T1CameraGrenade" : %GInv;
         %player.setInventory( %GInv, 30 );
      }
   }

   // if player is buying cameras, show how many are already deployed
   if(%client.favorites[%client.grenadeIndex] $= "Deployable Camera")
   {
      %maxDep = $TeamDeployableMax[DeployedCamera];
      %depSoFar = $TeamDeployedCount[%client.player.team, DeployedCamera];
      if(Game.numTeams > 1)
         %msTxt = "Your team has "@%depSoFar@" of "@%maxDep@" Deployable Cameras placed.";
      else
         %msTxt = "You have placed "@%depSoFar@" of "@%maxDep@" Deployable Cameras.";
      messageClient(%client, 'MsgTeamDepObjCount', %msTxt);
   }

   for ( %i = 0; %i < getFieldCount( %client.mineIndex ); %i++ )
   {
      %MInv = $NameToInv[%client.favorites[getField( %client.mineIndex, %i )]];
      if ( !($InvBanList[DeployInv, %MInv]) &&  !$InvBanList[%cmt, %MInv])
         %player.setInventory( %MInv, 30 );
   }
   if ( !($InvBanList[DeployInv, Beacon]) && !($InvBanList[%cmt, Beacon]) )
      %player.setInventory( Beacon, 20 ); // z0dd - ZOD, 4/24/02. 400 was a bit much, changed to 20.
   if ( !($InvBanList[DeployInv, RepairKit]) && !($InvBanList[%cmt, RepairKit]) )
      %player.setInventory( RepairKit, 1 );
   if ( !($InvBanList[DeployInv, TargetingLaser]) && !($InvBanList[%cmt, TargetingLaser]) )
      %player.setInventory( TargetingLaser, 1 );

   // players cannot buy deployable station packs from a deployable inventory station
   %packChoice = $NameToInv[%client.favorites[%client.packIndex]];
   if ( !($InvBanList[DeployInv, %packChoice]) && !$InvBanList[%cmt, %packChoice]){
      if(%packChoice $= "RepairPack"){
         %player.setInventory( "t1RepairPack", 1, 1);
      }
      else if(%packChoice $= "TurretIndoorDeployable" || %packChoice $= "TurretOutdoorDeployable"){
            %client.player.setInventory( "T1RemoteTurret", 1, 1);
      }
      else{
         %player.setInventory( %packChoice, 1 );
      }
   }

   // if this pack is a deployable that has a team limit, warn the purchaser
	// if it's a deployable turret, the limit depends on the number of players (deployables.cs)
	if(%packChoice $= "TurretIndoorDeployable" || %packChoice $= "TurretOutdoorDeployable")
		%maxDep = countTurretsAllowed(%packChoice);
	else
	   %maxDep = $TeamDeployableMax[%packChoice];
   if((%maxDep !$= "") && (%packChoice !$= "InventoryDeployable"))
   {
      %depSoFar = $TeamDeployedCount[%client.player.team, %packChoice];
      %packName = %client.favorites[%client.packIndex];

      if(Game.numTeams > 1)
         %msTxt = "Your team has "@%depSoFar@" of "@%maxDep SPC %packName@"s deployed.";
      else
         %msTxt = "You have deployed "@%depSoFar@" of "@%maxDep SPC %packName@"s.";

      messageClient(%client, 'MsgTeamDepObjCount', %msTxt);
   }

   if(%prevPack > 0)
   {
      // if player had a "forbidden" pack (such as a deployable inventory station)
      // BEFORE visiting a deployed inventory station AND still has that pack chosen
      // as a favorite, give it back
      if((%packChoice $= %prevPack.item) && ($InvBanList[DeployInv, %packChoice]))
         %player.setInventory( %prevPack.item, 1 );
   }

   if(%packChoice $= "AmmoPack")
      t1InvAmmoPackPass(%client);
}

//-------------------------------------------------------------------------------------
function T1getAmmoStationLovin(%client)
{
   // z0dd - ZOD, 4/24/02. This function was quite a mess, needed rewrite
   %cmt = $CurrentMissionType;

   // weapons
   for(%i = 0; %i < %client.player.weaponSlotCount; %i++)
   {
      %weapon = %client.player.weaponSlot[%i];
      if ( %weapon.image.ammo !$= "" )
         %client.player.setInventory( %weapon.image.ammo, 999 );
   }

   // grenades
   for(%i = 0; $InvGrenade[%i] !$= ""; %i++) // z0dd - ZOD, 5/27/03. Clear them all in one pass
      %client.player.setInventory($NameToInv[$InvGrenade[%i]], 0);

   for ( %i = 0; %i < getFieldCount( %client.grenadeIndex ); %i++ )
   {
      %client.player.lastGrenade = $NameToInv[%client.favorites[getField( %client.grenadeIndex, %i )]];
   }
   %grenType = %client.player.lastGrenade;
   if(%grenType $= "")
   {
      %grenType = Grenade;
   } 
   if ( !($InvBanList[%cmt, %grenType]) ){
      %grenType = (%grenType $= "Grenade") ? "T1Grenade" : %grenType;
      %grenType = (%grenType $= "Mine") ? "T1Mine" : %grenType;
      %grenType = (%grenType $= "CameraGrenade") ? "T1CameraGrenade" : %grenType;
      %client.player.setInventory( %grenType, 30 );
   }

   if(%grenType $= "Deployable Camera")
   {
      %maxDep = $TeamDeployableMax[DeployedCamera];
      %depSoFar = $TeamDeployedCount[%client.player.team, DeployedCamera];
      if(Game.numTeams > 1)
         %msTxt = "Your team has "@%depSoFar@" of "@%maxDep@" Deployable Cameras placed.";
      else
         %msTxt = "You have placed "@%depSoFar@" of "@%maxDep@" Deployable Cameras.";
      messageClient(%client, 'MsgTeamDepObjCount', %msTxt);
   }

   // Mines
   for(%i = 0; $InvMine[%i] !$= ""; %i++) // z0dd - ZOD, 5/27/03. Clear them all in one pass
       %client.player.setInventory($NameToInv[$InvMine[%i]], 0);

   for ( %i = 0; %i < getFieldCount( %client.mineIndex ); %i++ )
   {
      %client.player.lastMine = $NameToInv[%client.favorites[getField( %client.mineIndex, %i )]];
   }
   %mineType = %client.player.lastMine;
   if(%mineType $= "")
   {
      %mineType = Mine;
   }
   if ( !($InvBanList[%cmt, %mineType]) )
      %client.player.setInventory( %mineType, 30 );

   // miscellaneous stuff -- Repair Kit, Beacons, Targeting Laser
   if ( !($InvBanList[%cmt, RepairKit]) )
      %client.player.setInventory( RepairKit, 1 );

   if ( !($InvBanList[%cmt, Beacon]) )
      %client.player.setInventory( Beacon, 20 );

   if ( !($InvBanList[%cmt, TargetingLaser]) )
      %client.player.setInventory( TargetingLaser, 1 );

   if( %client.player.getMountedImage($BackpackSlot) $= "T1AmmoPack" )
      t1InvAmmoPackPass(%client);
}


function t1InvAmmoPackPass(%client)
{
   // "normal" ammo stuff (everything but mines and grenades)
   for ( %idx = 0; %idx < T1AmmoPack.NumAmmoItems; %idx++ ) 
   {
      %ammo = T1AmmoPack.AmmoItem[%idx];
      %client.player.t1IncInventory(%ammo, T1AmmoPack.max[%ammo]);
   }
   //our good friends, the grenade family *SIGH*
   // first find out what type of grenade the player has selected
   %grenFav = %client.favorites[getField(%client.grenadeIndex, 0)];
   if((%grenFav !$= "EMPTY") && (%grenFav !$= "INVALID"))
      %client.player.t1IncInventory($NameToInv[%grenFav], T1AmmoPack.max[$NameToInv[%grenFav]]);
   // now the same check for mines
   %mineFav = %client.favorites[getField(%client.mineIndex, 0)];
   if((%mineFav !$= "EMPTY") && (%mineFav !$= "INVALID") && !($InvBanList[%cmt, Mine]))
      %client.player.t1IncInventory($NameToInv[%mineFav], T1AmmoPack.max[$NameToInv[%mineFav]]);
}

function ShapeBase::t1clearInventory(%this)
{
   // z0dd - ZOD, 5/18/03. Auto cleanup of weapons and ammo. Streamline
   for(%i = 0; %i < $WeaponsHudCount; %i++)
   {
      %this.setInventory($WeaponsHudData[%i, itemDataName], 0);
      if($WeaponsHudData[%i, ammoDataName] !$= "")
         %this.setInventory($WeaponsHudData[%i, ammoDataName], 0);
   }
   // Handhelds, cleared through $InventoryHudData for the same reason the weapons loop
   // above uses $WeaponsHudData: loadRetInfo EXTENDS both lists with the T1 items, so
   // iterating them picks the T1 copies up automatically.
   //
   // The old code walked $InvGrenade and $InvMine instead, mapping through $NameToInv --
   // which only ever yields RETAIL names (Grenade, CameraGrenade, Mine, ...). Nothing
   // extends those lists, so T1Grenade, T1Mine, T1Beacon and T1CameraGrenade survived
   // every clear. A player kept the spawn kit's T1Grenade through every station buy, and
   // since Grenade is $InvGrenade[0] it won the first-match in ShapeBase::use, so the
   // grenade key threw a grenade no matter which handheld was actually selected.
   //
   // This also covers RepairKit ($InventoryHudData[2]) and Beacon ([7]), which the two
   // literal setInventory calls used to handle on their own.
   for(%i = 0; %i < $InventoryHudCount; %i++)
      %this.setInventory($InventoryHudData[%i, itemDataName], 0);

   // take away any pack the player has
   %curPack = %this.getMountedImage($BackpackSlot);
   if(%curPack > 0)
      %this.setInventory(%curPack.item, 0);
}   


$TeamDeployableMax[T1MineDeployed] = 25;//classic default


datablock ExplosionData(T1MineExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   playSpeed      = 1.0;
   sizes[0] = "0.5 0.5 0.5";
   sizes[1] = "0.5 0.5 0.5";
   soundProfile   = T1MortarExpSound;
   faceViewer     = true;

   emitter[0] = MineExplosionSmokeEmitter;
   emitter[1] = MineCrescentEmitter;

   shakeCamera = true;
   camShakeFreq = "8.0 7.0 9.0";
   camShakeAmp = "50.0 50.0 50.0";
   camShakeDuration = 1.0;
   camShakeRadius = 10.0;
};

datablock ItemData(T1MineDeployed)
{
   className = Weapon;
   shapeFile = "t1mine.dts";
   mass = 0.75;
   elasticity = 0.2;
   friction = 0.8; // z0dd - ZOD, 9/27/03. was 0.6
   pickupRadius = 3;
   maxDamage = 0.01; // z0dd - ZOD, 9/27/03. was 0.2
   explosion = T1MineExplosion;
   underwaterExplosion = UnderwaterMineExplosion;
   indirectDamage = 0.57; // z0dd - ZOD, 7/14/03. Slight increase to dmg. Was 0.55
   damageRadius = 10.0; // z0dd - ZOD, 7/14/03. Slight increase to det range. Was 6.0
   radiusDamageType = $DamageType::Mine;
   kickBackStrength = 1500;
   aiAvoidThis = true;
   dynamicType = $TypeMasks::DamagableItemObjectType;
   spacing = 6.0; // how close together mines can be
   proximity = 2.5; // how close causes a detonation (by player/vehicle)
   armTime = 2200; // 2.2 seconds to arm a mine after it comes to rest
   maxDepCount = 5; // try to deploy this many times before detonating. // z0dd - ZOD, 9/27/02. Was 9

   computeCRC = true;
};

datablock ItemData(T1Mine)
{     
   className = HandInventory;
   catagory = "Handheld";
   shapeFile = "t1mine.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.7;
   pickupRadius = 2;

   thrownItem = T1MineDeployed;
   pickUpName = "some mines";
   isMine = true; // z0dd - ZOD, 5/19/03. New param.
   computeCRC = true;
};


function T1MineDeployed::onThrow(%this, %mine, %thrower)
{
   error("t1MineDeployed::onThrow");
   %mine.armed = false;
   %mine.damaged = 0;
   %mine.detonated = false;
   %mine.depCount = 0;
   %mine.theClient = %thrower.client;
   $TeamDeployedCount[%mine.sourceObject.team, T1MineDeployed]++; // z0dd - ZOD, 8/13/02, Moved this from deployMineCheck to here. Fixes mine count bug

   schedule(1500, %mine, "T1deployMineCheck", %mine, %thrower);
}

function T1deployMineCheck(%mineObj, %player)
{
   if(%mineObj.depCount > %mineObj.getDatablock().maxDepCount)
      t1ExplodeMine(%mineObj, true);
   
   // wait until the mine comes to rest
   if(%mineObj.getVelocity() $= "0 0 0")
   {
      // 2-second delay before mine is armed -- let deploy thread play out etc.
      schedule(%mineObj.getDatablock().armTime, %mineObj, "t1ArmDeployedMine", %mineObj);

      // check for other deployed mines in the vicinity
      InitContainerRadiusSearch(%mineObj.getWorldBoxCenter(), %mineObj.getDatablock().spacing, $TypeMasks::ItemObjectType);
      while((%itemObj = containerSearchNext()) != 0)
      {
         if(%itemObj == %mineObj)
            continue;
         %ioType = %itemObj.getDatablock().getName();
         if(%ioType $= "MineDeployed")
            schedule(100, %mineObj, "t1ExplodeMine", %mineObj, true);
         else
            continue;
      }
      // play "deploy" thread
      %mineObj.playThread(0, "deploy");
      serverPlay3D(MineDeploySound, %mineObj.getTransform());
      %mineTeam = %mineObj.sourceObject.team;
      //$TeamDeployedCount[%mineTeam, MineDeployed]++; // z0dd - ZOD, 8/13/02, Moved the increment to MineDeployed::onThrow. Fixes mine count bug
      if($TeamDeployedCount[%mineTeam, T1MineDeployed] > $TeamDeployableMax[T1MineDeployed])
      {   
         messageClient( %player.client, '', 'Maximum allowable mines deployed.' );
         schedule(100, %mineObj, "t1ExplodeMine", %mineObj, true);
      }
      else
      {
         //start the thread that keeps checking for objects near the mine...
         t1MineCheckVicinity(%mineObj);

         //let the AI know *after* it's come to rest...
         AIDeployMine(%mineObj);

         //let the game know there's a deployed mine
         Game.notifyMineDeployed(%mineObj);
      }
   }
   else
   {
      //schedule this deploy check again a little later
      %mineObj.depCount++;
      schedule(500, %mineObj, "t1deployMineCheck", %mineObj, %player);
   }
}

// prob not needed but renamed it be there own thing in case its used in a mod that does not ahve these for whatever reasion 
function t1ArmDeployedMine(%mine)
{
   %mine.armed = true;
}

function t1MineCheckVicinity(%mine)
{
   // this function is called after the mine has been deployed. It will check the
   // immediate area around the mine (2.5 meters at present) for players or vehicles
   // passing by, and detonate if any are found. This is to extend the range of the
   // mine so players don't have to collide with them to set them off.

   // don't bother to check if mine isn't armed yet
   if(%mine.armed)
   {
      // don't keep checking if mine is already detonating
      if(!%mine.boom)
      {
         // the actual check for objects in the area
         %mineLoc = %mine.getWorldBoxCenter();
         %masks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType;
         %detonateRange = %mine.getDatablock().proximity;
         %noExplode = 0;
         InitContainerRadiusSearch(%mineLoc, %detonateRange, %masks);
         while((%tgt = containerSearchNext()) != 0) 
         {
            if(!$TeamDamage)
            {
               if(%mine.team == %tgt.team)
                  %noExplode = 1;
            }
            if(%noExplode == 0)
            {
               %mine.detonated = true;
               schedule(50, %mine, "t1ExplodeMine", %mine, false);
               break;
            }
         }
      }
   }
   // if nothing set off the mine, schedule another check
   if(!%mine.detonated)
      schedule(300, %mine, "t1MineCheckVicinity", %mine);
}

function t1ExplodeMine(%mo, %noDamage)
{
   %mo.noDamage = %noDamage;
   %mo.setDamageState(Destroyed);
}

function T1MineDeployed::onCollision(%data, %obj, %col)
{
   // don't detonate if mine isn't armed yet
   if(!%obj.armed)
      return;

   // don't detonate if mine is already detonating
   if(%obj.boom)
      return;

   %noExplode = 0;
   //check to see what it is that collided with the mine
   %struck = %col.getClassName();
   if(%struck $= "Player" || %struck $= "WheeledVehicle" || %struck $= "FlyingVehicle")
   {
      if(!$teamDamage)
      {
         if(%obj.team == %col.getOwnerClient().team)
            %noExplode = 1;
      }
      if(%noExplode == 0)
      {
         //error("Mine detonated due to collision with #"@%col@" ("@%struck@"); armed = "@%obj.armed);
         t1ExplodeMine(%obj, false);
      }
   }
}

function T1MineDeployed::damageObject(%data, %targetObject, %sourceObject, %position, %amount, %damageType)
{
   // -----------------------------
   // z0dd - ZOD, 5/09/04. If gameplay changes in affect, no mine disc
   if($Host::ClassicLoadMineChanges)
   {
      if(!%targetObject.armed)
         return;
   }
   // -----------------------------
     
   if(%targetObject.boom)
      return;

   %targetObject.damaged += %amount;

   if(%targetObject.damaged >= %data.maxDamage)
   {   
      %targetObject.setDamageState(Destroyed);
   }
}

function T1MineDeployed::onDestroyed(%data, %obj, %lastState)
{
   %obj.boom = true;
   %mineTeam = %obj.team;
   $TeamDeployedCount[%mineTeam, T1MineDeployed]--;
   // %noDamage is a boolean flag -- don't want to set off all other mines in
   // vicinity if there's a "mine overload", so apply no damage/impulse if true
   if(!%obj.noDamage)
       RadiusExplosion(%obj,
                      %obj.getPosition(),
                      %data.damageRadius,
                      %data.indirectDamage,
                      %data.kickBackStrength,
                      %obj.sourceObject,
                      %data.radiusDamageType);

   %obj.schedule(600, "delete");
}

datablock ExplosionData(T1HandGrenadeExplosion)
{
   soundProfile   = T1GLExpSound;

   emitter[0] = HandGrenadeExplosionSmokeEmitter;
   emitter[1] = HandGrenadeSparkEmitter;

   subExplosion[0] = HandGrenadeSubExplosion1;
   subExplosion[1] = HandGrenadeSubExplosion2;
   
   shakeCamera = true;
   camShakeFreq = "12.0 13.0 11.0";
   camShakeAmp = "35.0 35.0 35.0";
   camShakeDuration = 1.0;
   camShakeRadius = 15.0;
}; 

datablock ItemData(T1GrenadeThrown){
	className = Weapon;
	shapeFile = "t1Grenade.dts";
	mass = 0.7;
	elasticity = 0.2;
   friction = 1;
   pickupRadius = 2;
   maxDamage = 0.5;
	explosion = T1HandGrenadeExplosion;
	underwaterExplosion = UnderwaterHandGrenadeExplosion;
   indirectDamage      = 0.4;
   damageRadius        = 10.0;
   radiusDamageType    = $DamageType::Grenade;
   kickBackStrength    = 2000;

   computeCRC = true;

};

datablock ItemData(T1Grenade){
	className = HandInventory;
	catagory = "Handheld";
	shapeFile = "grenade.dts";
	mass = 0.7;
	elasticity = 0.2;
   friction = 1;
   pickupRadius = 2;
   thrownItem = T1GrenadeThrown;
	pickUpName = "some grenades";
	isGrenade = true;

   computeCRC = true;

};

function T1GrenadeThrown::onThrow(%this, %gren){
   AIGrenadeThrown(%gren);
   %gren.playThread(0, "deploy");
   %gren.detThread = schedule(1500, %gren, "detonateGrenade", %gren);
}

function T1GrenadeThrown::onCollision(%data, %obj, %col){
   // do nothing
}


package StarsiegeTribes{// annoyingly had to use package do to fixed use of names
   function ShapeBase::use(%this, %data){
     %val = parent::use(%this, %data);
      if(!%val && isObject(%this) && isObject(%data)) {
         if(%data $= Grenade && %this.inv[T1Grenade] > 0){
            %data = T1Grenade;
         }
         else if(%data $= Mine && %this.inv[T1Mine] > 0) {
            %data = T1Mine;
         }
         else if(%data $= Beacon && %this.inv[T1Beacon] > 0) {
            %data = T1Beacon;
         }
         else if(%data $= Grenade && %this.inv[T1CameraGrenade] > 0) {
            %data = T1CameraGrenade;
         }
         error("ShapeBase::use: data = "@%data@" inv = "@%this.inv[%data.getName()]);
         // default case
         if (%this.inv[%data.getName()] > 0) {
            %data.onUse(%this);
            return true;
         }
         return false;
      }
     return %val;
   }
};
if(!isActivePackage(StarsiegeTribes)){
   activatePackage(StarsiegeTribes);
}


datablock ShapeBaseImageData(T1AmmoPackImage)
{
   shapeFile = "t1AmmoPack.dts";
   item = T1AmmoPack;
   mountPoint = 1;
   offset = "0 0 0";
};

datablock ItemData(T1AmmoPack)
{
   className = Pack;
   catagory = "Packs";
   shapeFile = "t1AmmoPack.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   rotate = true;
   image = "T1AmmoPackImage";
	pickUpName = "an ammo pack";

   computeCRC = true;

   // uses t2 ammo  settings in ammopack.cs
	max[T1PlasmaAmmo] = 30;
	max[T1ChaingunAmmo] = 150;
	max[T1DiscAmmo] = 15;
	max[T1GrenadeLauncherAmmo] = 15;
	max[T1MortarAmmo] = 10;
	max[MissileLauncherAmmo] = 4;
	max[T1Grenade] = 10;
	max[ConcussionGrenade] = 10;
	max[FlashGrenade] = 10;
	max[FlareGrenade] = 10;
	max[CameraGrenade] = 0;
   max[T1Mine] = 5;
	max[RepairKit] = 1;

   AmmoItem[0] = T1PlasmaAmmo;
   AmmoItem[1] = T1ChaingunAmmo;
   AmmoItem[2] = T1DiscAmmo;
   AmmoItem[3] = T1GrenadeLauncherAmmo;
   AmmoItem[4] = T1MortarAmmo;
   AmmoItem[5] = T1MissileLauncherAmmo;
   AmmoItem[6] = RepairKit;

   NumAmmoItems = 7;

   GrenAmmoType[0] = T1Grenade;
   GrenAmmoType[1] = ConcussionGrenade;
   GrenAmmoType[2] = FlashGrenade;
   GrenAmmoType[3] = FlareGrenade;

   NumGrenTypes = 4;
};

function Player::t1MaxInventory(%this,%data)
{
   %max = ShapeBase::maxInventory(%this,%data);
   if (%this.getInventory(T1AmmoPack)){
      %max += T1AmmoPack.max[%data.getName()];
   }
   return %max;
}

function ShapeBase::t1IncInventory(%this, %data, %amount)
{
   if(isObject(%data)) // z0dd - ZOD, 5/18/03. Console spam fix
   {
      %max = %this.t1MaxInventory(%data);
      %cv = %this.inv[%data.getName()];
      // if(%data.getName() $= "PlasmaAmmo"){
      //    %this.save("meow.cs");
      // }
      if (%cv < %max) {
         if (%cv + %amount > %max)
            %amount = %max - %cv;
         %this.setInventory(%data,%cv + %amount,1);
         error(%data.getName() SPC %cv + %amount);
         %data.incCatagory(%this); // Inc the players weapon count
         return %max;
      }
      return 0;
   }
}

function T1AmmoPack::onPickup(%this,%pack,%player,%amount)
{
   error("T1AmmoPack::onPickup");
	// %this = AmmoPack datablock
	// %pack = AmmoPack object number
	// %player = player
	// %amount = 1

	for (%idx = 0; %idx < T1AmmoPack.NumAmmoItems; %idx++)
	{
	   %ammo = T1AmmoPack.AmmoItem[%idx];
      if (%pack.inv[%ammo] > 0)
      {
         %amount = %pack.getInventory(%ammo);
         %player.t1IncInventory(%ammo,%amount);
         %pack.setInventory(%ammo,0);
      }
		else if(%pack.inv[%ammo] == -1)
		{
			// this particular type of ammo has already been exhausted for this pack;
			// don't give the player any
		}
      else
      {
         // Assume it's full if no inventory has been assigned
         %player.t1IncInventory(%ammo,%this.max[%ammo]);
		}
   }
	// now check what type grenades (if any) player is carrying
	%gFound = false;
	for (%i = 0; %i < T1AmmoPack.NumGrenTypes; %i++)
	{
		%gType = T1AmmoPack.GrenAmmoType[%i];
		if(%player.inv[%gType] > 0)
		{
			%gFound = true;
			%playerGrenType = %gType;
			break;
		}
	}
	// if player doesn't have any grenades at all, give 'em whatever the ammo pack has
	if(!%gFound)
	{
		%given = false;
		for(%j = 0; %j < T1AmmoPack.NumGrenTypes; %j++)
		{
			%packGren = T1AmmoPack.GrenAmmoType[%j];
			if(%pack.inv[%packGren] > 0)
			{
				// pack has some of these grenades
	         %player.t1IncInventory(%packGren, %pack.getInventory(%packGren));
	         %pack.setInventory(%packGren, 0);
				%given = true;
				break;
			}
			else if(%pack.inv[%packGren] == -1)
			{
				// the pack doesn't have any of this type of grenades
			}
			else
			{
				// pack has full complement of this grenade type
	         %player.t1IncInventory(%packGren, %this.max[%packGren]);
				%given = true;
			}
			if(%given)
				break;
		}
	}
	else
	{
		// player had some amount of grenades before picking up pack
		if(%pack.inv[%playerGrenType] > 0)
		{
			// pack has 1 or more of this type of grenade
         %player.t1IncInventory(%playerGrenType, %pack.getInventory(%playerGrenType));
         %pack.setInventory(%playerGrenType, 0);
		}
		else if(%pack.inv[%playerGrenType] == -1)
		{
			// sorry Chester, the pack is out of these grenades.
		}
		else
		{
			// pack is uninitialized for this grenade type -- meaning it has full count
			%player.t1IncInventory(%playerGrenType, %this.max[%playerGrenType]);
		}
	}
	// now see if player had mines selected and if they're allowed in this mission type
	%mineFav = %player.client.favorites[getField(%player.client.mineIndex, 0)];
	if ( ( $InvBanList[$CurrentMissionType, "Mine"] !$= "1" ) 
	  && !( ( %mineFav $= "EMPTY" ) || ( %mineFav $= "INVALID" ) ) )
	{
		// player has selected mines, and they are legal in this mission type
		if(%pack.inv[Mine] > 0)
		{
			// and the pack has some mines in it! bonus!
			%player.t1IncInventory(Mine, %pack.getInventory(Mine));
			%pack.setInventory(Mine, 0);
		}
		else if(%pack.inv[Mine] == -1)
		{
			// no mines left in the pack. do nothing.
		}
		else
		{
			// assume it's full of mines if no inventory has been assigned
			%player.t1IncInventory(Mine,%this.max[Mine]);
		}
	}
}

function T1AmmoPack::onThrow(%this,%pack,%player)
{
	// %this = AmmoPack datablock
	// %pack = AmmoPack object number
	// %player = player

	%player.throwAmmoPack = 1;
	t1dropAmmoPack(%pack, %player);
	// do the normal ItemData::onThrow stuff -- sound and schedule deletion
   serverPlay3D(ItemThrowSound, %player.getTransform());
   %pack.schedulePop();
}

function T1AmmoPack::onInventory(%this,%player,%value)
{
   error("T1AmmoPack::onInventory");
	// %this = AmmoPack
	// %player = player
	// %value = 1 if gaining a pack, 0 if losing a pack

	// the below test is necessary because this function is called everytime the ammo
	// pack gains or loses an item
	if(%player.getClassName() $= "Player")
	{
		if(!%value)
			if(%player.throwAmmoPack == 1)
			{
				%player.throwAmmoPack = 0;
				// ammo stuff already handled by AmmoPack::onThrow
			}
			else
			{
				// ammo pack was sold at inventory station -- no need to worry about
				// setting the ammo in the pack object (it doesn't exist any more)
				t1dropAmmoPack(-1, %player);
			}
	}
   Pack::onInventory(%this,%player,%value);
}

function t1dropAmmoPack(%packObj, %player)
{
	// %packObj = Ammo Pack object number if pack is being thrown, -1 if sold at inv station
	// %player = player object

	for(%i = 0; %i < T1AmmoPack.NumAmmoItems; %i++)
	{
		%ammo = T1AmmoPack.AmmoItem[%i];
		// %pAmmo == how much of this ammo type the player has
		%pAmmo = %player.getInventory(%ammo);
		// %pMax == how much of this ammo type the player's datablock says can be carried
		%pMax = %player.getDatablock().max[%ammo];
		if(%pAmmo > %pMax)
		{
			// if player has more of this ammo type than datablock's max...
			if(%packObj > 0)
			{
				// put ammo that player can't carry anymore in pack
				%packObj.setInventory(%ammo, %pAmmo - %pMax);
			}
			// set player to max for this ammo type
			%player.setInventory(%ammo, %pMax);
		}
		else
		{
			if(%packObj > 0)
			{
				// the pack gets -1 of this ammo type -- else it'll assume it's full
				// can't use setInventory() because it won't allow values less than 1
				%packObj.inv[%ammo] = -1;
			}
		}
	}
	// and, of course, a pass for the grenades. Whee.
	%gotGren = false;
	// check to see what kind of grenades the player has.
	for(%j = 0; %j < T1AmmoPack.NumGrenTypes; %j++)
	{
		%gType = T1AmmoPack.GrenAmmoType[%j];
		if(%player.inv[%gType] > 0)
		{
			%gotGren = true;
			%playerGren = %gType;
			break;
		}
		else
		{
			// ammo pack only carries type of grenades that player who bought it (or picked
			// it up) had at the time -- all else are zeroed out (value of -1)
			if(%packObj > 0)
				%packObj.inv[%gType] = -1;
		}
	}
	if(%gotGren)
	{
		%pAmmo = %player.getInventory(%playerGren);
		%pMax = %player.getDatablock().max[%playerGren];
		if(%pAmmo > %pMax)
		{
			if(%packObj > 0)
				%packObj.setInventory(%playerGren, %pAmmo - %pMax);
			%player.setInventory(%playerGren, %pMax);
		}
		else
			if(%packObj > 0)
				%packObj.inv[%playerGren] = -1;
	}
	else
	{
		// player doesn't have any grenades at all. nothing needs to be done here.
	}
	// crap. forgot the mines.
	if(%player.getInventory(Mine) > %player.getDatablock().max[Mine])
	{
		// if player has more mines than datablock's max...
		if(%packObj > 0)
		{
			// put mines that player can't carry anymore in pack
			%packObj.setInventory(Mine, %player.getInventory(Mine) - %player.getDatablock().max[Mine]);
		}
		// set player to max mines
		%player.setInventory(Mine, %player.getDatablock().max[Mine]);
	}
	else
	{
		if(%packObj > 0)
		{
			// the pack gets -1 for mines -- else it'll assume it's full
			// can't use setInventory() because it won't allow values less than 1
			%packObj.inv[Mine] = -1;
		}
	}
}


datablock ShapeBaseImageData(T1EnergyPackImage)
{
   shapeFile = "t1EnergyPack.dts";
   item = T1EnergyPack;
   mountPoint = 1;
   offset = "0 0 0";
   rotation = "0 0 0 0";
   rechargeRateBoost = 0.15;

	stateName[0] = "default";
	stateSequence[0] = "activation";
};

datablock ItemData(T1EnergyPack)
{
   className = Pack;
   catagory = "Packs";
   shapeFile = "t1EnergyPack.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   rotate = true;
   image = "T1EnergyPackImage";
	pickUpName = "an energy pack";

   computeCRC = true;

};

function T1EnergyPackImage::onMount(%data, %obj, %node)
{
	%obj.setRechargeRate(%obj.getRechargeRate() + %data.rechargeRateBoost);
   %obj.hasEnergyPack = true; // set for sniper check
}

function T1EnergyPackImage::onUnmount(%data, %obj, %node)
{
	%obj.setRechargeRate(%obj.getRechargeRate() - %data.rechargeRateBoost);
   %obj.hasEnergyPack = "";
}

function T1EnergyPack::onPickup(%this, %obj, %shape, %amount)
{
	// created to prevent console errors
}



datablock AudioProfile(T1ShieldPackActivateSound)
{
	filename = "t1sounds/shield_on.wav";
	description = ClosestLooping3d;
   preload = true;
};

datablock ShapeBaseImageData(T1ShieldPackImage)
{
   shapeFile = "t1ShieldPack.dts";
   item = T1ShieldPack;
   mountPoint = 1;
   offset = "0 0 0";

   usesEnergy = true;
   minEnergy = 3;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";
	
	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateSequence[1] = "fire";
	stateSound[1] = T1ShieldPackActivateSound;
   stateEnergyDrain[1] = 9;
	stateTransitionOnTriggerUp[1] = "Deactivate";
   stateTransitionOnNoAmmo[1] = "Deactivate";

	stateName[2] = "Deactivate";
	stateScript[2] = "onDeactivate";
	stateTransitionOnTimeout[2] = "Idle";
};

datablock ItemData(T1ShieldPack)
{
   className = Pack;
   catagory = "Packs";
   shapeFile = "t1ShieldPack.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   rotate = true;
   image = "T1ShieldPackImage";
	pickUpName = "a shield pack";

   computeCRC = true;
};

function T1ShieldPackImage::onMount(%data, %obj, %node)
{
}

function T1ShieldPackImage::onUnmount(%data, %obj, %node)
{
	%obj.setImageTrigger(%node, false);
   %obj.isShielded = "";
}

function T1ShieldPackImage::onActivate(%data, %obj, %slot)
{
   messageClient(%obj.client, 'MsgShieldPackOn', '\c2Shield pack on.');
   %obj.isShielded = true;
   if ( !isDemo() )
      commandToClient( %obj.client, 'setShieldIconOn' );
}

function T1ShieldPackImage::onDeactivate(%data, %obj, %slot)
{
   messageClient(%obj.client, 'MsgShieldPackOff', '\c2Shield pack off.');
	%obj.setImageTrigger(%slot,false);
   %obj.isShielded = "";
   if ( !isDemo() )
      commandToClient( %obj.client, 'setShieldIconOff' );
}

function T1ShieldPack::onPickup(%this, %obj, %shape, %amount)
{
	// created to prevent console errors
}



// datablock AudioProfile(T1SensorJammerActivateSound)
// {
//    filename = "fx/packs/sensorjammerpack_on.wav";
//    description = ClosestLooping3d;
//    preload = true;
//    effect = SensorJammerPackActivateEffect;
// };

datablock ShapeBaseImageData(T1SensorJammerPackImage)
{
   shapeFile = "t1SensorJammerPack.dts";
   item = T1SensorJammerPack;
   mountPoint = 1;
   offset = "0 0 0";

   usesEnergy = true;
   minEnergy = 3;

   stateName[0] = "Idle";
   stateTransitionOnTriggerDown[0] = "Activate";
   
   stateName[1] = "Activate";
   stateScript[1] = "onActivate";
   stateSequence[1] = "fire";
   stateSound[1] = SensorJammerActivateSound;
   stateEnergyDrain[1] = 10.5;
   stateTransitionOnTriggerUp[1] = "Deactivate";
   stateTransitionOnNoAmmo[1] = "Deactivate";

   stateName[2] = "Deactivate";
   stateScript[2] = "onDeactivate";
   stateTransitionOnTimeout[2] = "Idle";
};

datablock ItemData(T1SensorJammerPack)
{
   className = Pack;
   catagory = "Packs";
   shapeFile = "t1SensorJammerPack.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
   rotate = true;
   image = "T1SensorJammerPackImage";
   pickUpName = "a sensor jammer pack";

   computeCRC = true;
};

function T1SensorJammerPackImage::onMount(%data, %obj, %slot)
{
    setTargetSensorData(%obj.client.target, JammerSensorObjectPassive);
}

function T1SensorJammerPackImage::onUnmount(%data, %obj, %slot)
{
    setTargetSensorData(%obj.client.target, PlayerSensor);
    %obj.setImageTrigger(%slot, false);
}

function T1SensorJammerPackImage::onActivate(%data, %obj, %slot)
{
   messageClient(%obj.client, 'MsgSensorJammerPackOn', '\c2Sensor jammer pack on.');
   setTargetSensorData(%obj.client.target, JammerSensorObjectActive);
   // z0dd - ZOD, 9/29/02. Removed T2 demo code from here
   commandToClient( %obj.client, 'setSenJamIconOn' );

   %obj.setJammerFX( true );
}

function T1SensorJammerPackImage::onDeactivate(%data, %obj, %slot)
{
   messageClient(%obj.client, 'MsgSensorJammerPackOff', '\c2Sensor jammer pack off.');
   %obj.setImageTrigger(%slot, false);

   // ----------------------------------------------------------------------
   // z0dd - ZOD, 4/25/02. This function is actually getting called AFTER 
   // ::onUnmount. We must check to see what the players current sensor data
   // is, then if it is NOT PlayerSensor, set to passive jam, bug fix.
   if(getTargetSensorData(%obj.client.target).getName() !$= "PlayerSensor")
      setTargetSensorData(%obj.client.target, JammerSensorObjectPassive);
   // ----------------------------------------------------------------------

   // z0dd - ZOD, 9/29/02. Removed T2 demo code from here
   commandToClient( %obj.client, 'setSenJamIconOff' );

   %obj.setJammerFX( false );
}


datablock ItemData(T1Beacon)
{
   className = HandInventory;
   catagory = "Misc";
   shapeFile = "t1beacon.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.8;
   pickupRadius = 1;
   pickUpName = "a deployable beacon";

   computeCRC = true;

};

datablock StaticShapeData(T1DeployedBeacon) : StaticShapeDamageProfile
{
   shapeFile = "t1beacon.dts";
   explosion = DeployablesExplosion;
   maxDamage = 0.45;
   disabledLevel = 0.45;
   destroyedLevel = 0.45;
   targetNameTag = 'beacon';
   deployedObject = true;
   dynamicType = $TypeMasks::SensorObjectType;
   debrisShapeName = "debris_generic_small.dts";
   debris = SmallShapeDebris;
   damageScale[$DamageType::Mine] = 1.0; // z0dd - ZOD, 5/17/03. Kill beacons that mark mines.
};

function T1DeployedBeacon::onDestroyed(%data, %obj, %prevState)
{
   if(%obj.getBeaconType() $= "friend")
      %bType = "MarkerBeacon";
   else
      %bType = "TargetBeacon";
   $TeamDeployedCount[%obj.team, %bType]--;
   %obj.schedule(500, delete);
}

function T1Beacon::onUse(%data, %obj)
{
   error("T1Beacon::onUse");
   // look for 3 meters along player's viewpoint for interior or terrain
   %searchRange = 3.0;
   %mask = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::StaticShapeObjectType | $TypeMasks::ForceFieldObjectType;
   // get the eye vector and eye transform of the player
   %eyeVec = %obj.getEyeVector();
   %eyeTrans = %obj.getEyeTransform();
   // extract the position of the player's camera from the eye transform (first 3 words)
   %eyePos = posFromTransform(%eyeTrans);
   // normalize the eye vector
   %nEyeVec = VectorNormalize(%eyeVec);
   // scale (lengthen) the normalized eye vector according to the search range
   %scEyeVec = VectorScale(%nEyeVec, %searchRange);
   // add the scaled & normalized eye vector to the position of the camera
   %eyeEnd = VectorAdd(%eyePos, %scEyeVec);
   // see if anything gets hit
   %searchResult = containerRayCast(%eyePos, %eyeEnd, %mask, 0);
   if(!%searchResult )
   {
      // no terrain/interior collision within search range
      if(%obj.inv[%data.getName()] > 0)
         messageClient(%obj.client, 'MsgBeaconNoSurface', '\c2Cannot place beacon. Too far from surface.');
      return 0;
   }
   else
   {
      %searchObj = GetWord(%searchResult, 0);
      if(%searchObj.getType() & ($TypeMasks::StaticShapeObjectType | $TypeMasks::ForceFieldObjectType) )
      {
         // if there's already a beacon where player is aiming, switch its type
         // otherwise, player can't deploy a beacon there
         if(%searchObj.getDataBlock().getName() $= T1DeployedBeacon ||  %searchObj.getDataBlock().getName() $= DeployedBeacon)
            switchBeaconType(%searchObj);
         else
            messageClient(%obj.client, 'MsgBeaconNoSurface', '\c2Cannot place beacon. Not a valid surface.');
         return 0;
      }
      else if(%obj.inv[%data.getName()] <= 0) 
         return 0;   
   }
   // newly deployed beacons default to "target" type
   if($TeamDeployedCount[%obj.team, TargetBeacon] >= $TeamDeployableMax[TargetBeacon])
   {
      messageClient(%obj.client, 'MsgDeployFailed', '\c2Your team\'s control network has reached its capacity for this item.~wfx/misc/misc.error.wav');
      return 0;
   }
   %terrPt = posFromRaycast(%searchResult);
   %terrNrm = normalFromRaycast(%searchResult);

   %intAngle = getTerrainAngle(%terrNrm);  // getTerrainAngle() function found in staticShape.cs
   %rotAxis = vectorNormalize(vectorCross(%terrNrm, "0 0 1"));
   if (getWord(%terrNrm, 2) == 1 || getWord(%terrNrm, 2) == -1)
      %rotAxis = vectorNormalize(vectorCross(%terrNrm, "0 1 0"));
   %rotation = %rotAxis @ " " @ %intAngle;

   %obj.decInventory(%data, 1);
   %depBeac = new BeaconObject() {
      dataBlock = "T1DeployedBeacon";
      position = VectorAdd(%terrPt, VectorScale(%terrNrm, 0.05));
      rotation = %rotation;
   };
   $TeamDeployedCount[%obj.team, TargetBeacon]++;
   
   %depBeac.playThread($AmbientThread, "ambient");
   %depBeac.team = %obj.team;
   %depBeac.sourceObject = %obj;

   // give it a team target
   %depBeac.setTarget(%depBeac.team);
   MissionCleanup.add(%depBeac);
}

$TeamDeployableMax[T1PulseSensorDeployable]    = 15;
$TeamDeployableMax[T1MotionSensorDeployable]   = 15;


datablock StaticShapeData(T1DeployedMotionSensor) : StaticShapeDamageProfile
{
   className = Sensor;
   shapeFile = "t1moSensor.dts";
   maxDamage = 0.6;
   destroyedLevel = 0.6;
   disabledLevel = 0.4;
   explosion = DeployablesExplosion;
   dynamicType = $TypeMasks::SensorObjectType;

   deployedObject = true;

   cmdCategory = "DSupport";
   cmdIcon = CMDSensorIcon;
   cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";
   targetNameTag = 'Deployable Motion';
   targetTypeTag = 'Sensor';
   sensorData = DeployMotionSensorObj;
   sensorRadius = DeployMotionSensorObj.detectRadius;
   sensorColor = "9 136 255";
   deployAmbientThread = true;

   debrisShapeName = "debris_generic_small.dts";
   debris = DeployableDebris;
   heatSignature = 0;
};

datablock ShapeBaseImageData(T1MotionSensorDeployableImage)
{
   shapeFile = "t1beacon.dts";
   item = T1MotionSensorDeployable;
   mountPoint = 1;
   offset = "0 0 0";
   rotation = "1 0 0 -90";
   deployed = T1DeployedMotionSensor;

   stateName[0] = "Idle";
   stateTransitionOnTriggerDown[0] = "Activate";

   stateName[1] = "Activate";
   stateScript[1] = "onActivate";
   stateTransitionOnTriggerUp[1] = "Idle";

   maxDepSlope = 360;
   deploySound = MotionSensorDeploySound;
   emap = true;
   heatSignature = 1;

   minDeployDis                       =  0.5; 
   maxDeployDis                       =  5.0;  //meters from body
};

datablock ItemData(T1MotionSensorDeployable)
{
   className = Pack;
   catagory = "Deployables";
   shapeFile = "t1beacon.dts";
   mass = 2.0;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 1;
   rotate = false;
   image = "T1MotionSensorDeployableImage";
   pickUpName = "a motion sensor pack";

   computeCRC = true;
   emap = true;
   heatSignature = 0;

   //maxSensors = 3;
   maxSensors = 2;
};

function T1DeployedMotionSensor::checkDeployPos(%this, %xform){
   %xform = setWord(%xform, 2, getWord(%xform,2)+0.1);
   return parent::checkDeployPos(%this, %xform);
}
function T1MotionSensorDeployableImage::onDeploy(%item, %plyr, %slot)
{
   %plyr.deploySensors--;
   %plyr.client.updateSensorPackText(%plyr.deploySensors);
   if(%plyr.deploySensors <= 0)
   {
      // take the deployable off the player's back and out of inventory
      %plyr.unmountImage(%slot);
      %plyr.decInventory(%item.item, 1);  
   }
  return parent::onDeploy(%item, %plyr, %slot); 
}

function T1DeployedMotionSensor::onDestroyed(%this, %obj, %prevState)
{
   //%obj.hide(true);
   Parent::onDestroyed(%this, %obj, %prevState);
   $TeamDeployedCount[%obj.team, T1MotionSensorDeployable]--;
   %obj.schedule(500, "delete");
}

function T1MotionSensorDeployableImage::getInitialRotation(%item, %plyr)
{
   %rotAxis = vectorNormalize(vectorCross(%item.surfaceNrm, "0 0 1"));
   if (getWord(%item.surfaceNrm, 2) == 1 || getWord(%item.surfaceNrm, 2) == -1)
      %rotAxis = vectorNormalize(vectorCross(%item.surfaceNrm, "0 1 0"));     
   return %rotAxis SPC mACos(vectorDot(%item.surfaceNrm, "0 0 1"));
}

function T1MotionSensorDeployable::onPickup(%this, %pack, %player, %amount)
{
   // %this = Sensor pack datablock
   // %pack = Sensor pack object number
   // %player = player
   // %amount = amount picked up (1)

   if(%pack.sensors $= "")
   {
      // assume that this is a pack that has been placed in a mission
      // this case was handled in ::onInventory below (max sensors);
   }
   else
   {
      // find out how many sensor were in the pack
      %player.deploySensors = %pack.sensors;
      %player.client.updateSensorPackText(%player.deploySensors);
   }
}

function T1MotionSensorDeployable::onThrow(%this,%pack,%player)
{
   // %this = Sensor pack datablock
   // %pack = Sensor pack object number
   // %player = player

   %player.throwSensorPack = 1;
   %pack.sensors = %player.deploySensors;
   %player.deploySensors = 0;
   %player.client.updateSensorPackText(%player.deploySensors);
   // do the normal ItemData::onThrow stuff -- sound and schedule deletion
   serverPlay3D(ItemThrowSound, %player.getTransform());
   %pack.schedulePop();
}

function T1MotionSensorDeployable::onInventory(%this,%player,%value)
{
   // %this = Sensor pack datablock
   // %player = player
   // %value = 1 if gaining a pack, 0 if losing a pack

   if(%player.getClassName() $= "Player")
   {
      if(%value)
      {
         // player picked up or bought a motion sensor pack
         %player.deploySensors = %this.maxSensors;
         %player.client.updateSensorPackText(%player.deploySensors);
      }
      else
      {
         // player dropped or sold a motion sensor pack
         if(%player.throwSensorPack)
         {
            // player threw the pack
            %player.throwSensorPack = 0;
            // everything handled in ::onThrow above
         }
         else
         {
            //the pack was sold at an inventory station, or unmounted because the player
            // used all the sensors
            %player.deploySensors = 0;
            %player.client.updateSensorPackText(%player.deploySensors);
         }
      }
   }
   Pack::onInventory(%this,%player,%value);
}



datablock StaticShapeData(T1DeployedPulseSensor) : StaticShapeDamageProfile
{
   className = Sensor;
   shapeFile = "t1PulseSensorDep.dts";
   maxDamage = 0.6;
   destroyedLevel = 0.6;
   disabledLevel = 0.4;
   explosion = DeployablesExplosion;
   dynamicType = $TypeMasks::SensorObjectType;

   deployedObject = true;

   cmdCategory = "DSupport";
   cmdIcon = CMDSensorIcon;
   cmdMiniIconName = "commander/MiniIcons/com_deploypulsesensor";
   targetNameTag = 'Deployable';
   targetTypeTag = 'Pulse Sensor';
   sensorData = DeployPulseSensorObj;
   sensorRadius = DeployPulseSensorObj.detectRadius;
   sensorColor = "255 194 9";
   deployAmbientThread = true;

   debrisShapeName = "debris_generic_small.dts";
   debris = DeployableDebris;
   heatSignature = 0;
};

datablock ShapeBaseImageData(T1PulseSensorDeployableImage)
{
   shapeFile = "t1PulseSensor.dts";
   item = T1PulseSensorDeployable;
   mountPoint = 1;
   offset = "0 0 0";
   rotation = "1 0 0 -90";
   deployed = T1DeployedPulseSensor;

   stateName[0] = "Idle";
   stateTransitionOnTriggerDown[0] = "Activate";

   stateName[1] = "Activate";
   stateScript[1] = "onActivate";
   stateTransitionOnTriggerUp[1] = "Idle";
   deploySound = SensorDeploySound;

   maxDepSlope = 40;
   emap = true;
   heatSignature = 0;

   minDeployDis                       =  0.5;
   maxDeployDis                       =  5.0;  //meters from body
};

datablock ItemData(T1PulseSensorDeployable)
{
   className = Pack;
   catagory = "Deployables";
   shapeFile = "t1PulseSensor.dts";
   mass = 2.0;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 1;
   rotate = false;
   image = "T1PulseSensorDeployableImage";
   pickUpName = "a pulse sensor pack";

   computeCRC = true;
   emap = true;

   maxSensors = 2;
};

function T1DeployedPulseSensor::onDestroyed(%this, %obj, %prevState)
{
   Parent::onDestroyed(%this, %obj, %prevState);
   $TeamDeployedCount[%obj.team, T1PulseSensorDeployable]--;
   %obj.schedule(300, "delete");
}

function T1PulseSensorDeployable::onPickup(%this, %pack, %player, %amount)
{
   // %this = Sensor pack datablock
   // %pack = Sensor pack object number
   // %player = player
   // %amount = amount picked up (1)

   if(%pack.sensors $= "")
   {
      // assume that this is a pack that has been placed in a mission
      // this case was handled in ::onInventory below (max sensors);
   }
   else
   {
      // find out how many sensor were in the pack
      %player.deploySensors = %pack.sensors;
      %player.client.updateSensorPackText(%player.deploySensors);
   }
}

function T1PulseSensorDeployable::onThrow(%this,%pack,%player)
{
   // %this = Sensor pack datablock
   // %pack = Sensor pack object number
   // %player = player

   %player.throwSensorPack = 1;
   %pack.sensors = %player.deploySensors;
   %player.deploySensors = 0;
   %player.client.updateSensorPackText(%player.deploySensors);
   // do the normal ItemData::onThrow stuff -- sound and schedule deletion
   serverPlay3D(ItemThrowSound, %player.getTransform());
   %pack.schedulePop();
}

function T1PulseSensorDeployable::onInventory(%this,%player,%value)
{
   // %this = Sensor pack datablock
   // %player = player
   // %value = 1 if gaining a pack, 0 if losing a pack

   if(%player.getClassName() $= "Player")
   {
      if(%value)
      {
         // player picked up or bought a motion sensor pack
         %player.deploySensors = %this.maxSensors;
         %player.client.updateSensorPackText(%player.deploySensors);
      }
      else
      {
         // player dropped or sold a motion sensor pack
         if(%player.throwSensorPack)
         {
            // player threw the pack
            %player.throwSensorPack = 0;
            // everything handled in ::onThrow above
         }
         else
         {
            //the pack was sold at an inventory station, or unmounted because the player
            // used all the sensors
            %player.deploySensors = 0;
            %player.client.updateSensorPackText(%player.deploySensors);
         }
      }
   }
   Pack::onInventory(%this,%player,%value);
}

// TSShapeConstructors.  These are what graft the 226 .dsq clips onto the five
// mesh-only .dts, and they are SimDataBlocks -- IMPLEMENT_CO_DATABLOCK_V1 in
// tsShapeConstruct.cc -- so creating them here on the server ghosts them to every
// client, which is how the clips reach a remote player.  Stock does the identical
// thing for light_male via scripts/player.cs.
//
// They must come BEFORE the PlayerData below: TSShapeConstructor::onAdd is a no-op
// once the shape resource reports getSequencesConstructed(), so if a PlayerData
// preloads the .dts first the clips are never imported -- and the armor then renders
// perfectly while refusing to animate, which looks nothing like a load-order bug.
//
// Inlined verbatim from what used to be shapes/<armor>.cs.  Those files are gone;
// if you regenerate the armors, the generator's output goes HERE.  Sequence numbering
// only has to be unique and gapless within each block -- it is not an engine slot.
datablock TSShapeConstructor(larmorDts)
{
   baseShape = "larmor.dts";
   sequence0 = "larmor_root.dsq root";
   sequence1 = "larmor_run.dsq run";
   sequence2 = "larmor_runback.dsq back";
   sequence3 = "larmor_side_left.dsq side";
   sequence4 = "larmor_looks.dsq looks";
   sequence5 = "larmor_apc_root.dsq apc_root";
   sequence6 = "larmor_apc_pilot.dsq apc_pilot";
   sequence7 = "larmor_crouch_root.dsq crouch_root";
   sequence8 = "larmor_crouch_looks.dsq crouch_looks";
   sequence9 = "larmor_crouch_forward.dsq crouch_forward";
   sequence10 = "larmor_crouch_side_left.dsq crouch_side_left";
   sequence11 = "larmor_crouch_die.dsq crouch_die";
   sequence12 = "larmor_jump_run.dsq jump";
   sequence13 = "larmor_fall.dsq fall";
   sequence14 = "larmor_landing.dsq land";
   sequence15 = "larmor_jet.dsq jet";
   sequence16 = "larmor_die_back.dsq death3";
   sequence17 = "larmor_die_blown_back.dsq death8";
   sequence18 = "larmor_die_chest.dsq death2";
   sequence19 = "larmor_die_head.dsq death1";
   sequence20 = "larmor_die_leg_right.dsq death7";
   sequence21 = "larmor_die_leg_left.dsq death6";
   sequence22 = "larmor_die_left_side.dsq death4";
   sequence23 = "larmor_die_grab_back.dsq die_grab_back";
   sequence24 = "larmor_die_right_side.dsq death5";
   sequence25 = "larmor_die_spin.dsq death11";
   sequence26 = "larmor_die_forward_kneel.dsq death9";
   sequence27 = "larmor_die_forward.dsq death10";
   sequence28 = "larmor_sign_over_here.dsq sign_over_here";
   sequence29 = "larmor_sign_stop.dsq sign_stop";
   sequence30 = "larmor_sign_point.dsq sign_point";
   sequence31 = "larmor_sign_salut.dsq sign_salut";
   sequence32 = "larmor_sign_retreat.dsq sign_retreat";
   sequence33 = "larmor_pda_access.dsq pda";
   sequence34 = "larmor_celebration_1.dsq cel1";
   sequence35 = "larmor_celebration_3.dsq cel3";
   sequence36 = "larmor_celebration_2.dsq cel2";
   sequence37 = "larmor_taunt_2.dsq cel7";
   sequence38 = "larmor_taunt_1.dsq cel4";
   sequence39 = "larmor_wave.dsq wave";
   sequence40 = "larmor_throw.dsq throw";
   sequence41 = "larmor_flyer_root.dsq flyer_root";
   sequence42 = "larmor_pose_kneel.dsq pose_kneel";
   sequence43 = "larmor_pose_stand.dsq pose_stand";
   sequence44 = "larmor_look.dsq look";
   sequence45 = "larmor_head.dsq head";
   sequence46 = "larmor_headside.dsq headside";
   sequence47 = "larmor_standjump.dsq standjump";
   sequence48 = "larmor_ski.dsq ski";
};

datablock TSShapeConstructor(marmorDts)
{
   baseShape = "marmor.dts";
   sequence0 = "marmor_root.dsq root";
   sequence1 = "marmor_run.dsq run";
   sequence2 = "marmor_runback.dsq back";
   sequence3 = "marmor_side_left.dsq side";
   sequence4 = "marmor_apc_root.dsq apc_root";
   sequence5 = "marmor_apc_pilot.dsq apc_pilot";
   sequence6 = "marmor_pda_access.dsq pda";
   sequence7 = "marmor_sign_retreat.dsq sign_retreat";
   sequence8 = "marmor_sign_over_here.dsq sign_over_here";
   sequence9 = "marmor_sign_point.dsq sign_point";
   sequence10 = "marmor_sign_stop.dsq sign_stop";
   sequence11 = "marmor_sign_salut.dsq sign_salut";
   sequence12 = "marmor_taunt_1.dsq cel4";
   sequence13 = "marmor_taunt_2.dsq cel7";
   sequence14 = "marmor_pose_kneel.dsq pose_kneel";
   sequence15 = "marmor_pose_stand.dsq pose_stand";
   sequence16 = "marmor_celebration_1.dsq cel1";
   sequence17 = "marmor_celebration_2.dsq cel2";
   sequence18 = "marmor_celebration_3.dsq cel3";
   sequence19 = "marmor_wave.dsq wave";
   sequence20 = "marmor_die_head.dsq death1";
   sequence21 = "marmor_die_chest.dsq death2";
   sequence22 = "marmor_die_left_side.dsq death4";
   sequence23 = "marmor_die_right_side.dsq death5";
   sequence24 = "marmor_die_blown_back.dsq death8";
   sequence25 = "marmor_die_back.dsq death3";
   sequence26 = "marmor_die_leg_right.dsq death7";
   sequence27 = "marmor_die_leg_left.dsq death6";
   sequence28 = "marmor_die_spin.dsq death11";
   sequence29 = "marmor_die_grab_back.dsq die_grab_back";
   sequence30 = "marmor_die_forward_kneel.dsq death9";
   sequence31 = "marmor_die_forward.dsq death10";
   sequence32 = "marmor_jet.dsq jet";
   sequence33 = "marmor_landing.dsq land";
   sequence34 = "marmor_jump_run.dsq jump";
   sequence35 = "marmor_fall_controlled.dsq fall";
   sequence36 = "marmor_looks.dsq looks";
   sequence37 = "marmor_throw.dsq throw";
   sequence38 = "marmor_look.dsq look";
   sequence39 = "marmor_head.dsq head";
   sequence40 = "marmor_headside.dsq headside";
   sequence41 = "marmor_standjump.dsq standjump";
   sequence42 = "marmor_ski.dsq ski";
};

datablock TSShapeConstructor(harmorDts)
{
   baseShape = "harmor.dts";
   sequence0 = "harmor_root.dsq root";
   sequence1 = "harmor_looks.dsq looks";
   sequence2 = "harmor_apc_root.dsq apc_root";
   sequence3 = "harmor_apc_pilot.dsq apc_pilot";
   sequence4 = "harmor_run.dsq run";
   sequence5 = "harmor_runback.dsq back";
   sequence6 = "harmor_side_left.dsq side";
   sequence7 = "harmor_jump_run.dsq jump";
   sequence8 = "harmor_throw.dsq throw";
   sequence9 = "harmor_pda_access.dsq pda";
   sequence10 = "harmor_jet.dsq jet";
   sequence11 = "harmor_fall.dsq fall";
   sequence12 = "harmor_landing.dsq land";
   sequence13 = "harmor_sign_over_here.dsq sign_over_here";
   sequence14 = "harmor_sign_point.dsq sign_point";
   sequence15 = "harmor_sign_retreat.dsq sign_retreat";
   sequence16 = "harmor_sign_stop.dsq sign_stop";
   sequence17 = "harmor_sign_salut.dsq sign_salut";
   sequence18 = "harmor_wave.dsq wave";
   sequence19 = "harmor_die_forward_kneel.dsq death9";
   sequence20 = "harmor_die_back.dsq death3";
   sequence21 = "harmor_die_blown_back.dsq death8";
   sequence22 = "harmor_die_chest.dsq death2";
   sequence23 = "harmor_die_forward.dsq death10";
   sequence24 = "harmor_die_grab_back.dsq die_grab_back";
   sequence25 = "harmor_die_head.dsq death1";
   sequence26 = "harmor_die_leg_left.dsq death6";
   sequence27 = "harmor_die_leg_right.dsq death7";
   sequence28 = "harmor_die_right_side.dsq death5";
   sequence29 = "harmor_die_left_side.dsq death4";
   sequence30 = "harmor_die_spin.dsq death11";
   sequence31 = "harmor_celebration_1.dsq cel1";
   sequence32 = "harmor_celebration_2.dsq cel2";
   sequence33 = "harmor_celebration_3.dsq cel3";
   sequence34 = "harmor_taunt_1.dsq cel4";
   sequence35 = "harmor_taunt_2.dsq cel7";
   sequence36 = "harmor_pose_kneel.dsq pose_kneel";
   sequence37 = "harmor_pose_stand.dsq pose_stand";
   sequence38 = "harmor_look.dsq look";
   sequence39 = "harmor_head.dsq head";
   sequence40 = "harmor_headside.dsq headside";
   sequence41 = "harmor_standjump.dsq standjump";
   sequence42 = "harmor_ski.dsq ski";
};

datablock TSShapeConstructor(lfemaleDts)
{
   baseShape = "lfemale.dts";
   sequence0 = "lfemale_root.dsq root";
   sequence1 = "lfemale_run.dsq run";
   sequence2 = "lfemale_runback.dsq back";
   sequence3 = "lfemale_side_left.dsq side";
   sequence4 = "lfemale_jet.dsq jet";
   sequence5 = "lfemale_fall.dsq fall";
   sequence6 = "lfemale_landing.dsq land";
   sequence7 = "lfemale_jump_run.dsq jump";
   sequence8 = "lfemale_looks.dsq looks";
   sequence9 = "lfemale_pda_access.dsq pda";
   sequence10 = "lfemale_throw.dsq throw";
   sequence11 = "lfemale_flyer_root.dsq flyer_root";
   sequence12 = "lfemale_apc_root.dsq apc_root";
   sequence13 = "lfemale_celebration_1.dsq cel1";
   sequence14 = "lfemale_celebration_2.dsq cel2";
   sequence15 = "lfemale_celebration_3.dsq cel3";
   sequence16 = "lfemale_taunt_1.dsq cel4";
   sequence17 = "lfemale_taunt_2.dsq cel7";
   sequence18 = "lfemale_wave.dsq wave";
   sequence19 = "lfemale_pose_kneel.dsq pose_kneel";
   sequence20 = "lfemale_pose_stand.dsq pose_stand";
   sequence21 = "lfemale_crouch_root.dsq crouch_root";
   sequence22 = "lfemale_crouch_looks.dsq crouch_looks";
   sequence23 = "lfemale_crouch_forward.dsq crouch_forward";
   sequence24 = "lfemale_crouch_side_left.dsq crouch_side_left";
   sequence25 = "lfemale_crouch_die.dsq crouch_die";
   sequence26 = "lfemale_die_forward.dsq death10";
   sequence27 = "lfemale_die_chest.dsq death2";
   sequence28 = "lfemale_die_back.dsq death3";
   sequence29 = "lfemale_die_blown_back.dsq death8";
   sequence30 = "lfemale_die_spin.dsq death11";
   sequence31 = "lfemale_die_head.dsq death1";
   sequence32 = "lfemale_die_left_side.dsq death4";
   sequence33 = "lfemale_die_right_side.dsq death5";
   sequence34 = "lfemale_die_leg_right.dsq death7";
   sequence35 = "lfemale_die_forward_kneel.dsq death9";
   sequence36 = "lfemale_die_leg_left.dsq death6";
   sequence37 = "lfemale_die_grab_back.dsq die_grab_back";
   sequence38 = "lfemale_sign_over_here.dsq sign_over_here";
   sequence39 = "lfemale_sign_point.dsq sign_point";
   sequence40 = "lfemale_sign_retreat.dsq sign_retreat";
   sequence41 = "lfemale_sign_salut.dsq sign_salut";
   sequence42 = "lfemale_sign_stop.dsq sign_stop";
   sequence43 = "lfemale_look.dsq look";
   sequence44 = "lfemale_head.dsq head";
   sequence45 = "lfemale_headside.dsq headside";
   sequence46 = "lfemale_standjump.dsq standjump";
   sequence47 = "lfemale_ski.dsq ski";
};

datablock TSShapeConstructor(mfemaleDts)
{
   baseShape = "mfemale.dts";
   sequence0 = "mfemale_root.dsq root";
   sequence1 = "mfemale_looks.dsq looks";
   sequence2 = "mfemale_apc_root.dsq apc_root";
   sequence3 = "mfemale_apc_pilot.dsq apc_pilot";
   sequence4 = "mfemale_pose_stand.dsq pose_stand";
   sequence5 = "mfemale_pose_kneel.dsq pose_kneel";
   sequence6 = "mfemale_pda_access.dsq pda";
   sequence7 = "mfemale_throw.dsq throw";
   sequence8 = "mfemale_taunt_1.dsq cel4";
   sequence9 = "mfemale_taunt_2.dsq cel7";
   sequence10 = "mfemale_celebration_1.dsq cel1";
   sequence11 = "mfemale_celebration_2.dsq cel2";
   sequence12 = "mfemale_celebration_3.dsq cel3";
   sequence13 = "mfemale_sign_point.dsq sign_point";
   sequence14 = "mfemale_sign_over_here.dsq sign_over_here";
   sequence15 = "mfemale_sign_retreat.dsq sign_retreat";
   sequence16 = "mfemale_sign_salut.dsq sign_salut";
   sequence17 = "mfemale_sign_stop.dsq sign_stop";
   sequence18 = "mfemale_wave.dsq wave";
   sequence19 = "mfemale_run.dsq run";
   sequence20 = "mfemale_runback.dsq back";
   sequence21 = "mfemale_side_left.dsq side";
   sequence22 = "mfemale_jump_run.dsq jump";
   sequence23 = "mfemale_fall.dsq fall";
   sequence24 = "mfemale_landing.dsq land";
   sequence25 = "mfemale_jet.dsq jet";
   sequence26 = "mfemale_die_back.dsq death3";
   sequence27 = "mfemale_die_blown_back.dsq death8";
   sequence28 = "mfemale_die_chest.dsq death2";
   sequence29 = "mfemale_die_spin.dsq death11";
   sequence30 = "mfemale_die_leg_right.dsq death7";
   sequence31 = "mfemale_die_leg_left.dsq death6";
   sequence32 = "mfemale_die_head.dsq death1";
   sequence33 = "mfemale_die_right_side.dsq death5";
   sequence34 = "mfemale_die_left_side.dsq death4";
   sequence35 = "mfemale_die_grab_back.dsq die_grab_back";
   sequence36 = "mfemale_die_forward_kneel.dsq death9";
   sequence37 = "mfemale_die_forward.dsq death10";
   sequence38 = "mfemale_look.dsq look";
   sequence39 = "mfemale_head.dsq head";
   sequence40 = "mfemale_headside.dsq headside";
   sequence41 = "mfemale_standjump.dsq standjump";
   sequence42 = "mfemale_ski.dsq ski";
};

// onAdd() returns false SILENTLY when baseShape does not resolve, so say it out loud.
function t1CheckShapes(){
   %bad = "";
   if(!isObject(larmorDts))  %bad = %bad @ " larmorDts";
   if(!isObject(marmorDts))  %bad = %bad @ " marmorDts";
   if(!isObject(harmorDts))  %bad = %bad @ " harmorDts";
   if(!isObject(lfemaleDts)) %bad = %bad @ " lfemaleDts";
   if(!isObject(mfemaleDts)) %bad = %bad @ " mfemaleDts";
   if(%bad !$= "")
      error("retro armor: TSShapeConstructor(s) FAILED to register:" @ %bad @
            " -- baseShape did not resolve under shapes/, so those armors have ZERO" SPC
            "animation clips. Check the .dts/.dsq are loose in base/shapes/ and that no" SPC
            "stale shapes/*.cs.dso from the old t1armor.vl2 is shadowing them.");
}
t1CheckShapes();

// Race "TR1", so the stock name-building sites resolve to these.  Each one inherits
// its stock counterpart, so mass/damage/energy/loadout rules are untouched -- only
// the mesh changes.  There is no T1 heavy female mesh; stock has the datablock, so
// it maps to the male heavy the way the old mod did.
datablock PlayerData(LightMaleTR1Armor) : LightMaleHumanArmor {
   shapeFile = "larmor.dts";
   computeCRC = false;
};
datablock PlayerData(MediumMaleTR1Armor) : MediumMaleHumanArmor {
   shapeFile = "marmor.dts";
   computeCRC = false;
};
datablock PlayerData(HeavyMaleTR1Armor) : HeavyMaleHumanArmor {
   shapeFile = "harmor.dts";
   computeCRC = false;
};
datablock PlayerData(LightFemaleTR1Armor) : LightFemaleHumanArmor {
   shapeFile = "lfemale.dts";
   computeCRC = false;
};
datablock PlayerData(MediumFemaleTR1Armor) : MediumFemaleHumanArmor {
   shapeFile = "mfemale.dts";
   computeCRC = false;
};
datablock PlayerData(HeavyFemaleTR1Armor) : HeavyFemaleHumanArmor {
   shapeFile = "harmor.dts";
   computeCRC = false;
};

// Same test CTFGame::equip uses, against %client.team -- at createPlayer time the
// player object has no team yet, and %player.team is assigned from %client.team a
// few lines later anyway, so the two agree.
// Is this TEAM retro on this map? Split out of t1UseRetroArmor because the team skins
// below key off a bare team number: getTeamSkin is called for flags, holo projectors and
// score icons as well as for players, and none of those carry a client.
function t1RetroTeam(%team){
   if(!isObject(StarsiegeTribesMap))
      return false;
   // teamOnly 0 = the whole map is retro; otherwise only that one team is (the T1-vs-T2
   // map), and the other side must keep its stock armors and stock skins.
   if(StarsiegeTribesMap.teamOnly != 0 && StarsiegeTribesMap.teamOnly != %team)
      return false;
   return true;
}

function t1UseRetroArmor(%client){
   if(!$T1RetroArmor)
      return false;
   if(!t1RetroTeam(%client.team))
      return false;
   // Never hand out a datablock that is not there: `new Player(){dataBlock = ""}`
   // fails and the client spawns as nothing at all.
   return isObject(LightMaleTR1Armor);
}

// THE WHOLE SWAP IS ONE FIELD.  All three stock sites that pick an armor datablock
// build the name the same way --
//     %size @ %client.sex @ %client.race @ "Armor"
// (defaultGame.cs:446 createPlayer, player.cs:2982 Player::setArmor, and
// inventoryHud.cs:1053 getArmorDatablock) -- so setting race to "TR1" redirects
// spawning, inventory stations, buyFavorites, t1buyFavorites and the bots in one
// go, including any path not found while writing this.  Nothing else in the stock
// scripts reads .race except the "Bioderm" tests, which simply stop matching.
//
// Deliberately NOT %player.setDataBlock() after the fact: that would fix spawning
// only, and every station visit would put the stock armor back.
function t1SyncArmorRace(%client){
   if(%client.t1RealRace $= "")
      %client.t1RealRace = %client.race;
   if(t1UseRetroArmor(%client))
      %client.race = "TR1";
   else
      %client.race = %client.t1RealRace;
}

// Re-exec'd on every CreateServer, so deactivate BEFORE the block is re-parsed:
// redefining a function while its package is active is what stacks Parent:: chains
// on top of each other.  Deactivate -> redefine -> activate is the safe order.
if(isActivePackage(T1RetroArmorPkg)){
   deactivatePackage(T1RetroArmorPkg);
}
package T1RetroArmorPkg{
   // Every game type reaches this: CTF/Siege/etc. inherit it, and the types that
   // do define their own (DMGame, HuntersGame, RabbitGame) call
   // DefaultGame::createPlayer explicitly, which still lands on this override.
   function DefaultGame::createPlayer(%game, %client, %spawnLoc, %respawn){
      t1SyncArmorRace(%client);
      Parent::createPlayer(%game, %client, %spawnLoc, %respawn);
   }

   // TEAM SKINS: Storm gets beagle, Inferno gets dsword.
   //
   // No new textures needed -- beagle.* and dsword.* already ship for all five T1
   // armors (and for the stock ones), and both are first-class team skins with matching
   // BEagleLogo / DSwordLogo StaticShapeData, so the holo projectors that build
   // "<skin>Logo" resolve exactly the way 'base'/'baseb' do.
   //
   // Overriding CTFGame's and not DefaultGame's is deliberate: CTFGame::getTeamSkin
   // (CTFGame.cs:275) does NOT chain to DefaultGame -- it picks a skin from the mission's
   // musicTrack and returns, so an override on the parent would never run on a CTF map,
   // which is what the retro maps are.
   //
   // The skin codes are inline rather than in a global because these must be TAGGED
   // strings ('x', not "x"); to change a team's look, edit the literal here.
   function CTFGame::getTeamSkin(%game, %team){
      if($T1RetroSkin && t1RetroTeam(%team)){
         if(%team == 1) return 'beagle';    // Storm
         if(%team == 2) return 'dsword';    // Inferno
      }
      return Parent::getTeamSkin(%game, %team);
   }
};
activatePackage(T1RetroArmorPkg);

//client raycast fix not needed tho, only here for archrival
//TSShapeInstance::castRay
//memPatch("6bb017","eb00");
//memPatch("6bb1e0","eb00");
//memPatch("6bb1ff","eb00");

//TSShapeInstance::buildPolyList
//memPatch("6bac02","eb00");
//memPatch("6bad48","eb00");
//memPatch("6bad7e","eb00");


$T1FlagSndCount = 0;
function t1FlagSnd(%from, %to){
   $T1FlagSndFrom[$T1FlagSndCount] = %from;
   $T1FlagSndTo[$T1FlagSndCount] = %to;
   $T1FlagSndCount++;
}
t1FlagSnd("fx/misc/flag_snatch.wav",  "fx/misc/flagfriend.wav");   // you or an ally took theirs
t1FlagSnd("fx/misc/flag_taken.wav",   "fx/misc/flagenemy.wav");    // yours was taken
t1FlagSnd("fx/misc/flag_drop.wav",    "fx/misc/t1flagdrop.wav");
t1FlagSnd("fx/misc/flag_capture.wav", "fx/misc/flagcapture.wav");
t1FlagSnd("fx/misc/flag_return.wav",  "fx/misc/flagreturn.wav");
t1FlagSnd("fx/misc/flag_lost.wav",    "fx/misc/flagself.wav");

// The world flag, and the one mounted on the carrier.
//
// `: Flag` ALONE DOES NOT INHERIT THE SCRIPT CALLBACKS -- it only copies fields.
// compiledEval.cc:547 does exactly one thing with the parent, dataBlock->assignFieldsFrom
// (parent); there is no namespace link. Without the className below, Flag::onThrow and
// every other Flag:: callback would simply stop being reached once an object was swapped.
//
// `className` is what does it. GameBaseData::onAdd (gameBase.cc:53) does
//     Con::linkNamespaces(parent->mName, className);
//     Con::linkNamespaces(className, name);
// so declaring className = Flag makes T1Flag's parent namespace Flag, and T1Flag::onThrow
// falls through. This is also why the TR1 armors work: stock armors carry
// className = Armor, and the field copy above brings it along.
//
// computeCRC is off deliberately. Stock Flag has it ON, and leaving it on would require
// every client to hold a byte-identical t1baseflag.dts or fail the check. Turn it back on
// once the shape is settled and distributed.
datablock ItemData(T1Flag) : Flag
{
   className = Flag;
   shapeFile = "t1baseflag.dts";
   computeCRC = false;
};
datablock ShapeBaseImageData(T1FlagImage) : FlagImage
{
   offset = "0 -0.12 0";
   className = FlagImage;
   shapeFile = "t1baseflag.dts";
   computeCRC = false;
};


function t1IsFlag(%obj){
   if(!isObject(%obj) || !isObject(%obj.getDataBlock()))
      return false;
   %n = %obj.getDataBlock().getName();
   return (%n $= "Flag" || %n $= "T1Flag");
}

function t1RetroFlagOn(%team){
   if(!$T1RetroFlag)
      return false;
   if(!isObject(T1Flag))
      return false;
   return t1RetroTeam(%team);
}

// This engine has no strReplace.
function t1StrSwap(%s, %find, %rep){
   %p = strPos(%s, %find);
   if(%p < 0)
      return %s;
   return getSubStr(%s, 0, %p) @ %rep @
          getSubStr(%s, %p + strlen(%find), strlen(%s));
}

function t1FlagSwapText(%text){
   for(%i = 0; %i < $T1FlagSndCount; %i++)
      %text = t1StrSwap(%text, $T1FlagSndFrom[%i], $T1FlagSndTo[%i]);
   return %text;
}


function t1FlagMsg(%msg){
   if(!$T1RetroFlag || !isObject(StarsiegeTribesMap))
      return %msg;

   %tag = getTag(%msg);
   if(%tag $= %msg)
      return t1FlagSwapText(%msg);      // plain text, not a tagged literal

   if($T1FlagMsgDone[%tag])
      return $T1FlagMsgMap[%tag];

   %text = getTaggedString(%msg);
   %new = t1FlagSwapText(%text);
   $T1FlagMsgDone[%tag] = true;
   $T1FlagMsgMap[%tag] = (%new $= %text) ? %msg : addTaggedString(%new);
   return $T1FlagMsgMap[%tag];
}

// --- flag replacement -------------------------------------------------------
// setDataBlock crashes on a flag Item (reported), and hooking Flag::objectiveInit did
// not fire at all -- LCTFGame.cs carries its own Flag::objectiveInit inside a package
// and that copy never calls Parent::, so an override there is silently bypassed. Both
// routes are dead, so: delete the stock flag and build ours in its place.
//
// Driven from T1StartObj::onAdd, which is the one hook we KNOW runs on a retro map --
// it is what names StarsiegeTribesMap, and the armor swap depends on it. It is a
// StaticShapeData callback, so no game-type package can shadow it.
//
// It runs on a SCHEDULE, not inline, for two reasons: flags have no team until
// objectiveInit (defaultGame.cs runs it after setUpTeams), and objectiveInit is reached
// from SimGroup::objectiveInit walking the group BY INDEX -- deleting and adding during
// that walk shifts the indices and silently skips objects.

$T1FlagTodoCount = 0;

function t1CollectFlags(%group){
   for(%i = 0; %i < %group.getCount(); %i++){
      %o = %group.getObject(%i);
      if(%o.getClassName() $= "SimGroup"){
         t1CollectFlags(%o);
         continue;
      }
      if(!isObject(%o.getDataBlock()))
         continue;
      if(%o.getDataBlock().getName() $= "Flag" && t1RetroTeam(%o.team)){
         $T1FlagTodo[$T1FlagTodoCount] = %o;
         $T1FlagTodoCount++;
      }
   }
}

// Everything CTF hangs off a flag is rebuilt by running objectiveInit on the new object,
// so the only manual work is tearing down what pointed at the old one.
function t1ReplaceFlag(%old){
   %team = %old.team;
   %xf = %old.getTransform();
   %grp = %old.getGroup();
   %skinned = %old.isTeamSkinned;

   if(isObject(%old.waypoint))
      %old.waypoint.delete();
   if(isObject(%old.trigger))
      %old.trigger.delete();

   %new = new Item(){
      dataBlock = T1Flag;
   };
   %new.team = %team;
   %new.isTeamSkinned = %skinned;
   if(isObject(%grp))
      %grp.add(%new);
   else
      MissionCleanup.add(%new);
   %new.setTransform(%xf);

   %old.delete();

   // rebuilds waypoint, trigger, target, originalPosition, $TeamFlag and $flagPos --
   // through whichever objectiveInit is actually active, so LCTF's copy is fine too
   %new.objectiveInit();
   error("T1FLAG replaced team " @ %team @ " -> " @ %new @
         " db=" @ %new.getDataBlock().getName());
   return %new;
}

// Retries because the delay needed depends on how long the mission takes to load; a flag
// with no team yet means objectiveInit has not run, so there is nothing to replace.
function t1SwapFlags(%tries){
   if(%tries $= "")
      %tries = 0;
   if(!$T1RetroFlag || !isObject(T1Flag) || !isObject(StarsiegeTribesMap))
      return;

   $T1FlagTodoCount = 0;
   if(isObject(MissionGroup))
      t1CollectFlags(MissionGroup);

   if($T1FlagTodoCount == 0){
      if(%tries < 20)
         schedule(500, 0, "t1SwapFlags", %tries + 1);
      else
         error("T1FLAG: gave up -- no stock Flag with a retro team found in MissionGroup");
      return;
   }
   for(%i = 0; %i < $T1FlagTodoCount; %i++)
      t1ReplaceFlag($T1FlagTodo[%i]);
   $T1FlagTodoCount = 0;
}

if(isActivePackage(T1RetroFlagPkg)){
   deactivatePackage(T1RetroFlagPkg);
}
package T1RetroFlagPkg{

   // Runs on every retro map -- see the flag replacement above. StaticShapeData
   // callback, so no game-type package can shadow it the way LCTFGame shadows
   // Flag::objectiveInit.
   function T1StartObj::onAdd(%this, %obj){
      Parent::onAdd(%this, %obj);
      schedule(500, 0, "t1SwapFlags");
   }

   // THROWING. ShapeBase::throwObject (inventory.cs:435) special-cases the flag THREE
   // times by literal datablock name, and a T1Flag matches none of them:
   //
   //   :444  clears %obj.static, so the anti-hover hack lets the flag be thrown at all
   //   :455  corpse throws use a random vector -- except for flags
   //   :478  scales the impulse x75 (flags have huge mass so shots do not punt them),
   //         AND sets flagTossWait, the 1-second grab delay
   //
   // Missing the last one is why the flag could not be thrown and was instantly
   // re-collected: no impulse scale, so it barely moved, and no toss delay, so the
   // thrower picked it straight back up.
   //
   // Copied from this install's inventory.cs with those three tests widened via
   // t1IsFlag. Everything else is byte-for-byte the stock body.
   function ShapeBase::throwObject(%this, %obj){
      if(!$MatchStarted)
         return;

      if(t1IsFlag(%obj)){
         %obj.static = false;
         if(Game.Class $= CTFGame || Game.Class $= PracticeCTFGame)
            %obj.searchSchedule = Game.schedule(10, "startFlagCollisionSearch", %obj);
      }

      %srcCorpse = (%this.getState() $= "Dead");
      if (%srcCorpse && !t1IsFlag(%obj))
      {
         %vec = (-1.0 + getRandom() * 2.0) SPC (-1.0 + getRandom() * 2.0) SPC getRandom();
         %vec = vectorScale(%vec, 10);
      }
      else
      {
         %eye = %this.getEyeVector();
         %vec = vectorScale(%eye, 20);
      }

      %dot = vectorDot("0 0 1", %eye);
      if (%dot < 0)
         %dot = -%dot;
      %vec = vectorAdd(%vec, vectorScale("0 0 12", 1 - %dot));

      %vec = vectorAdd(%vec, %this.getVelocity());
      %pos = getBoxCenter(%this.getWorldBox());

      if (t1IsFlag(%obj))
      {
         %vec = vectorScale(%vec, (%srcCorpse ? 40 : 75));
         %this.flagTossWait = true;
         %this.schedule(1000, resetFlagTossWait);
      }

      %obj.setTransform(%pos);
      %obj.applyImpulse(%pos, %vec);
      %obj.setCollisionTimeout(%this);
      %data = %obj.getDatablock();

      %data.onThrow(%obj, %this);

      AIThrowObject(%obj);
   }

   // The one thing the swap above would otherwise break: flag-defense scoring compares
   // the datablock NAME against the literal "Flag", so a T1Flag would silently stop
   // awarding defend points. Same logic, both names accepted.
   function CTFGame::testFlagDefend(%game, %victimID, %killerID){
      InitContainerRadiusSearch(%victimID.plyrPointOfDeath, %game.RADIUS_FLAG_DEFENSE,
                                $TypeMasks::ItemObjectType);
      %objID = containerSearchNext();
      while(%objID != 0){
         if(t1IsFlag(%objID) && (%objID.team == %killerID.team))
            return true;
         %objID = containerSearchNext();
      }
      return false;
   }

   // The CARRIED flag mounts as FlagImage, hardcoded in CTFGame::playerTouchEnemyFlag.
   // Intercepting the mount rather than overriding that 40-line function keeps this
   // working whatever else has been changed in CTFGame.cs. holdingFlag is assigned just
   // BEFORE the mount, so the flag team -- and therefore the skin -- is known here.
   // The name test is what stops the recursion: T1FlagImage inherits this namespace, so
   // without it T1FlagImage::onMount would land straight back in here.
   function FlagImage::onMount(%this, %obj, %slot){
      if(%this.getName() $= "FlagImage" && isObject(T1FlagImage)
         && isObject(%obj.holdingFlag) && t1RetroFlagOn(%obj.holdingFlag.team)){
         // remount next tick -- swapping the image from inside its own mount callback
         // re-enters the mount path while the engine is still walking it
         %obj.schedule(0, "mountImage", T1FlagImage, %slot, true,
                       Game.getTeamSkin(%obj.holdingFlag.team));
         return;
      }
      Parent::onMount(%this, %obj, %slot);
   }

   // Sound swap. Every flag sound in the game reaches a client through one of these five,
   // carried as "~w<path>" inside the message string.
   function messageClient(%client, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6,
                          %a7, %a8, %a9, %a10, %a11, %a12, %a13){
      Parent::messageClient(%client, %msgType, t1FlagMsg(%msgString), %a1, %a2, %a3, %a4,
                            %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
   function messageTeam(%team, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6,
                        %a7, %a8, %a9, %a10, %a11, %a12, %a13){
      Parent::messageTeam(%team, %msgType, t1FlagMsg(%msgString), %a1, %a2, %a3, %a4,
                          %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
   function messageTeamExcept(%client, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6,
                              %a7, %a8, %a9, %a10, %a11, %a12, %a13){
      Parent::messageTeamExcept(%client, %msgType, t1FlagMsg(%msgString), %a1, %a2, %a3,
                                %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
   function messageAll(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6,
                       %a7, %a8, %a9, %a10, %a11, %a12, %a13){
      Parent::messageAll(%msgType, t1FlagMsg(%msgString), %a1, %a2, %a3, %a4, %a5, %a6,
                         %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
   function messageAllExcept(%client, %team, %msgtype, %msgString, %a1, %a2, %a3, %a4,
                             %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13){
      Parent::messageAllExcept(%client, %team, %msgtype, t1FlagMsg(%msgString), %a1, %a2,
                               %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
};
activatePackage(T1RetroFlagPkg);

$TeamDeployableMax[T1DeployedCamera]  = 15;
datablock ItemData(T1CameraGrenadeThrown)
{
   shapeFile = "t1camera.dts";
   mass = 0.7;
   elasticity = 0.2;
   friction = 1;
   pickupRadius = 2;
   maxDamage = 0.8;
   sticky = true;
   emap = true;
   
};

datablock ItemData(T1CameraGrenade)
{
   className = HandInventory;
   catagory = "Handheld";
   shapeFile = "t1camera.dts";
   mass = 0.7;
   elasticity = 0.2;
   friction = 1;
   pickupRadius = 2;
   thrownItem = T1CameraGrenadeThrown;
   pickUpName = "a deployable camera";
   isGrenade = true; // z0dd - ZOD, 5/18/03. Was missing this parameter
   computeCRC = true;
   emap = true;
};

datablock TurretData(T1TurretDeployedCamera) : TurretDamageProfile
{
   className = CameraTurret;
   shapeFile = "t1camera.dts";

   mass = 0.7;
   maxDamage = 0.2;
   destroyedLevel = 0.2;
   disabledLevel = 0.2;
   repairRate = 0;
   explosion = CameraGrenadeExplosion;

   thetaMin = 0;
   thetaMax = 180;
   //thetaNull = 90;

	deployedObject = true;

   isShielded = false;
   energyPerDamagePoint = 40;
   maxEnergy = 30;
   renderWhenDestroyed = false;
   rechargeRate = 0.05;

   cameraDefaultFov = 90; // z0dd - ZOD, 4/24/02 Camera gren tweaks. was 150
   cameraMinFov = 5; // z0dd - ZOD, 4/24/02. Camera gren tweaks. was 150
   cameraMaxFov = 120; // z0dd - ZOD, 4/24/02. Camera gren tweaks. was 150
   
   neverUpdateControl = false;  // z0dd - ZOD, 4/24/02. Enable controllable camera view

   canControl = true;
   canObserve = false; // z0dd - ZOD, 4/24/02. Turned off 3rd person camera viewing.
   observeThroughObject = true;
   cmdCategory = "DSupport";
   cmdIcon = CMDCameraIcon;
   cmdMiniIconName = "commander/MiniIcons/com_camera_grey";
   targetNameTag = 'Deployed';
   targetTypeTag = 'Camera';
   sensorData = CameraSensorObject;
   sensorRadius = CameraSensorObject.detectRadius;

   firstPersonOnly = true;
   observeParameters = "0.5 4.5 4.5";

   debrisShapeName = "debris_generic_small.dts";
   debris = SmallShapeDebris;
};

function T1TurretDeployedCamera::onAdd(%this, %obj)
{
   Parent::onAdd(%this, %obj);   
   %obj.mountImage(DeployableCameraBarrel, 0, true);
	%obj.setRechargeRate(%this.rechargeRate);

   %obj.setAutoFire(false); // z0dd - ZOD, 4/17/02. Server crash fix related to controlable cameras
}

function T1TurretDeployedCamera::onDestroyed(%this, %obj, %prevState)
{
   Parent::onDestroyed(%this, %obj, %prevState);
   $TeamDeployedCount[%obj.team, T1DeployedCamera]--;
   // doesn't seem to delete itself, so...
   %obj.schedule(500, "delete");
}


function T1CameraGrenadeThrown::onThrow(%this, %gren)
{
   // schedule a check to see if the camera is at rest but not deployed
   %gren.checkCount = 0;
   %gren.velocCheck = %this.schedule($CameraDeployTime, "checkCameraDeploy", %gren);
}

function T1CameraGrenadeThrown::onStickyCollision(%data, %obj)
{
   cancel(%obj.velocCheck);
   %pos = %obj.getLastStickyPos();
   %norm = %obj.getLastStickyNormal();
   
   %intAngle = getTerrainAngle(%norm);  // staticShape.cs
   %rotAxis = vectorNormalize(vectorCross(%norm, "0 0 1"));
   if (getWord(%norm, 2) == 1 || getWord(%norm, 2) == -1)
      %rotAxis = vectorNormalize(vectorCross(%norm, "0 1 0"));

   %rotation = %rotAxis @ " " @ %intAngle;
   %dcSucc = activateT1Camera(%pos, %rotation, %obj.sourceObject, %obj.sourceObject.team);
   if(%dcSucc == 0)
      messageClient(%obj.sourceObject.client, 'MsgDeployFailed', '\c2Your team\'s control network has reached its capacity for this item.~wfx/misc/misc.error.wav');
   %obj.schedule(50,"delete");
}

function T1CameraGrenadeThrown::checkCameraDeploy(%this, %gren)
{
   %gren.checkCount++;
   if(VectorLen(%gren.getVelocity()) < $CameraMinVelocity)
   {
      // camera has come to rest but not deployed -- probably on a staticshape (station, gen, etc)
      // no resolution, so get rid of it
      %gren.schedule(50, "delete");
   }
   else if(%gren.checkCount >= $CameraDeployCheckMax)
   {
      // camera's still moving but it's been check several times -- it was thrown from too great
      // a height or off the edge of the world -- delete it
      %gren.schedule(50, "delete");
   }
   else
   {
      // check back in a little while
      %gren.velocCheck = %this.schedule($CameraDeployTime, "checkCameraDeploy", %gren);
   }
}

function activateT1Camera(%position, %rotation, %sourceObj, %team)
{
   if($TeamDeployedCount[%team, T1DeployedCamera] >= $TeamDeployableMax[T1DeployedCamera])
   {
      // team has too many cameras deployed already, don't deploy this one
      return 0;
   }
   %dCam = new Turret()
   {
      dataBlock = "T1TurretDeployedCamera";                            
      team = %team;
      needsNoPower = true;
      owner = %sourceObj.client;
      ownerHandle = %sourceObj.client.handle;
      position = %position;
      rotation = %rotation;
   };
   addToDeployGroup(%dCam);

   if(%dCam.getTarget() != -1)
      setTargetSensorGroup(%dCam.getTarget(), %team);

   %dCam.playAudio($DeploySound, CameraGrenadeAttachSound);

   // NOT %dCam.deploy(). A deployed camera gets its power from the deploy ANIMATION
   // ending, not from the power grid:
   //
   //     StaticShape::deploy          -> playThread($DeployThread, "deploy")
   //     StaticShapeData::onEndSequence -> if(%thread == $DeployThread) setSelfPowered()
   //
   // t1camera.dts has no sequence called "deploy" -- its one is named "activate", because
   // TurretData::preload (turret.cc:169) requires turn/elevate/activate and REFUSES to
   // load the datablock without all three. So deploy() would find no sequence, no thread
   // would run, onEndSequence would never fire, setSelfPowered() would never be called,
   // and the commander-map control check (serverCommanderMap.cs:65, %obj.isPowered())
   // reports "object is not powered".
   //
   // onEndSequence keys off the thread SLOT, not the sequence name, so playing the same
   // animation into the deploy slot restores the power grant. setSelfPowered is also
   // called outright so power never depends on an animation completing.
   %dCam.playThread($DeployThread, "activate");
   %dCam.setSelfPowered();
   %dCam.playThread($AmbientThread, "ambient");

   $TeamDeployedCount[%team, T1DeployedCamera]++;
   return 1;
}