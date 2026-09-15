//t2 ammo station
//Script By: DarkTiger
$AmmoStation::mode = 1; // ammo only 0 like a deployable invy 
datablock TriggerData(T2CtmStationTrig){
   tickPeriodMS =  32;
};

datablock StaticShapeData(T2AmmoDeployableObj) : StaticShapeDamageProfile
{
   className = Station;
   shapeFile = "t2DepAmmo.dts";
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

datablock ShapeBaseImageData(T2AmmoDeployableImage)
{
   mass = 12; // z0dd - ZOD, 7/17/02. large packs are too heavy enough with new physics. was 15
   emap = true;

   shapeFile = "t2DepAmmo_Pack.dts";
   item = T2AmmoDeployable;
   mountPoint = 1;
   offset = "0 0 -0.5";
   rotation = "0 0 1 180";
   deployed = T2AmmoDeployableObj;
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

$TeamDeployableMax[T2AmmoDeployable] = 6;
$TeamDeployableMin[T2AmmoDeployable] = 6;

datablock ItemData(T2AmmoDeployable)
{
   className = Pack;
   catagory = "Deployables";
   shapeFile = "t2DepAmmo_Pack.dts";
   mass = 3.0;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 1;
   rotate = false;
   image = "T2AmmoDeployableImage";
   pickUpName = "an ammo station pack";
   heatSignature = 0;

   computeCRC = true;
   emap = true;

};

function T2AmmoDeployableImage::getInitialRotation(%item, %plyr) {
   %rot = rotFromTransform(%plyr.getTransform());
   // Rotate 180 degrees around Z-axis (PI radians)
   // Multiply original rotation by the 180-degree Z rotation
   %newRot = MatrixMultiply("0 0 0" SPC %rot, "0 0 0" SPC "0 0 1" SPC 3.14159265359);
   return getWords(rotFromTransform(%newRot), 0, 3);
}

function T2AmmoDeployableImage::onDeploy(%item, %plyr, %slot){
   %obj = Parent::onDeploy(%item, %plyr, %slot);
   %obj.init = 0;
   %trigger = new Trigger()
   { 
      dataBlock = T2CtmStationTrig;
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
function T2AmmoDeployable::onCollision(%data,%obj,%col){
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
      %col.setInventory(T2AmmoDeployable, 1, true);
      
   }
}

function T2AmmoDeployableObj::onDestroyed(%this, %obj, %prevState){
   Parent::onDestroyed(%this, %obj, %prevState);
   //$TeamDeployedCount[%obj.team, T2AmmoDeployable]--;
   if(isObject(%obj.trigger)){   
      %obj.trigger.delete();
   }
   if(isObject(%obj.beacon)){   
      %obj.beacon.delete();
   }
   %obj.schedule(256, "delete");
}

function T2AmmoDeployableObj::onRemove(%this,%obj){
   $TeamDeployedCount[%obj.team, T2AmmoDeployable]--;
   if(isObject(%obj.trigger)){   
      %obj.trigger.delete();
   }
   if(isObject(%obj.beacon)){   
      %obj.beacon.delete();
   }
   Parent::onRemove(%this, %obj);
}


function T2CtmStationTrig::onEnterTrigger(%data, %trigger, %player){
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
         messageClient(%player.client, 'msgStationDenied', '\c2Access Denied -- Wrong team.~wfx/powered/station_denied.wav');
   }
   else if(%station.isDisabled()){
      messageClient(%player.client, 'msgStationDisabled', '\c2Station is disabled.~wfx/powered/station_denied.wav');
   }
   else if(!%station.isPowered()){
      messageClient(%player.client, 'msgStationNoPower', '\c2Station is not powered.~wfx/powered/station_denied.wav');
   }
   else if(%station.notDeployed){
      messageClient(%player.client, 'msgStationNotDeployed', '\c2Station is not deployed.');
   }
   else if(%station.isDestroyed){
      messageClient(%player.client, 'msgStationDestroyed', '\c2Station is destroyed.~wfx/powered/station_denied.wav');
   }
   else if(isObject(%station.curPlayer) && %station.curPlayer.getState() !$= "Dead"){
      messageClient(%player.client, 'msgStationInUse', '\c2Station is in use.~wfx/powered/station_denied.wav');
   }
   else if(isObject(%station) && %station.isEnabled() &&  %station.isPowered()){
      messageClient(%player.client, 'CloseHud', "", 'inventoryScreen');
      commandToClient(%player.client, 'TogglePlayHuds', true);
      if(%targetname $= "T2AmmoDeployableObj"){
         %station.playAudio(2, DepInvActivateSound);
         if($AmmoStation::mode){
            getAmmoStationLovin2(%player.client);
         }
         else{
            buyDeployableFavorites(%player.client);
         }
         %player.setVelocity("0 0 0");
         %oldRate = %player.getRepairRate();
         %player.setRepairRate(%oldRate + 0.00625);
         %station.playThread(2, "activate"); 
      }
      else if(%targetname $= "TAAInvyPad"){
         %station.playAudio(2, TAAInvyPadActivate);
        // %station.playAudio(3, invyPadRun);
         buyFavorites(%player.client);
         %plrRot = getWords(%player.getTransform(), 3, 6);
         %fvec = %station.getForwardVector();
         %sPos = vectorAdd(%station.getPosition(),"0 0 0.25");
         %player.setTransform(vectorAdd(%sPos,vectorScale(%fvec,0.1)) SPC %plrRot);
         %player.setvelocity("0 0 0");
         %player.setMoveState(true);
         %oldRate = %player.getRepairRate();
         %player.setRepairRate(%oldRate + 0.00625);
         %station.playThread(2, "activate");
         %station.curPlayer = %player;

      }
   }
   %trigger.lastObj = %player;
}

function TAAInvyPad::onEndSequence(%data, %obj, %thread){
   //error("TAAInvyPad::onEndSequence" SPC %data SPC %obj SPC %thread);
   if(%thread == 2 && isObject(%obj.curPlayer)){
      %player = %obj.curPlayer;
      %player.setMoveState(false);
   }
}

function T2CtmStationTrig::onleaveTrigger(%data, %trigger, %player){
   %station = %trigger.station;
   %targetname = %station.getDataBlock().getName(); 
   if(%player.team == %station.team && !%trigger.powerTrig){
      if(isObject(%station)){
         if(%targetname $= "TAAInvyPad"){
            if(!%station.isDestroyed && %station.isPowered() && !%station.isDisabled()){
               %station.playAudio(2, TAAInvyPadDeactivate); 
               %station.playThread(2, "deactivate");   
            }
            %station.curPlayer = "";
            %player.setRepairRate(0);
            //%station.stopAudio(3);
         }
         else{
            %station.stopThread(2);
            %player.setRepairRate(0);
         }
         if(%player.getMountedImage($WeaponSlot) == 0)
         {
            if(%player.inv[%player.lastWeapon]){
               %player.use(%player.lastWeapon);
            }
            else{ 
               %player.selectWeaponSlot( 0 );
            }
         }
      }
   }
}

function T2CtmStationTrig::onTickTrigger(%this, %triggerId){
 return;
}

function getAmmoStationLovin2(%client)
{
   // z0dd - ZOD, 4/24/02. This function was quite a mess, needed rewrite
   %cmt = $CurrentMissionType;

   // weapons
   for(%i = 0; %i < %client.player.weaponSlotCount; %i++)
   {
      %weapon = %client.player.weaponSlot[%i];
      if(%weapon.image.ammo $= "DarkAmmo"){
         if(!%weapon.isEx){
            %ammo = getField($darkWep[%client.wt, %client.wc],1);
            %bonusAmmo = ((%client.mod1 == 3) * mFloor(%ammo * 0.25)) + ((%client.mod2 == 3) * mFloor(%ammo * 0.5))  + ((%client.mod3 == 3) * mFloor(%ammo * 1));
            %client.player.setInventory("DarkAmmo", mFloor(%ammo + %bonusAmmo),true); 
         }
      }
      else if ( %weapon.image.ammo !$= "" ){
         %client.player.setInventory( %weapon.image.ammo, 999 );
      }
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
   if ( !($InvBanList[%cmt, %grenType]) )
      %client.player.setInventory( %grenType, 30 );

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

   if( %client.player.getMountedImage($BackpackSlot) $= "AmmoPack" )
      invAmmoPackPass(%client);
}

///////////////////////////////////////////////////////////////////////////
datablock AudioProfile(TAAInvyPadActivate)
{
   filename    = "taaInvyActivate.wav";
   description = AudioDefault3d;
   preload = true;
};
datablock AudioProfile(TAAInvyPadDeactivate)
{
   filename    = "taaInvyDeactivate.wav";
   description = AudioDefault3d;
   preload = true;
};
// datablock AudioProfile(TAAInvyPadRun)
// {
//    filename    = "t1sounds/inv_use.wav";
//    description = AudioDefaultLooping3d;
//    preload = true;
// };
datablock StaticShapeData(TAAInvyPad)
{
   catagory = "Stations";
   shapeFile = "invyStationTAA.dts";
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

function TAAInvyPad::onAdd(%this, %obj)
{
   Parent::onAdd(%this, %obj);
   %obj.setRechargeRate(%obj.getDatablock().rechargeRate);
   %trigger = new Trigger()
   {
      dataBlock = T2CtmStationTrig;
      polyhedron = "-0.75 0.75 0.1 1.5 0.0 0.0 0.0 -1.5 0.0 0.0 0.0 2.3";
   };             
   MissionCleanup.add(%trigger);
   %trigger.setTransform(%obj.getTransform());
   %trigger.station = %obj;
   %trigger.mainObj = %obj;
   %trigger.disableObj = %obj;
   %obj.trigger = %trigger;
}

function TAAInvyPad::onRemove(%this, %obj){
   parent::onRemove(%this, %obj);
   if(isObject(%obj.trigger)){
      %obj.trigger.delete();
   }
}
