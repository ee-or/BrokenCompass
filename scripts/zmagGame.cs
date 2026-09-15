datablock StaticShapeData(chrome)
{
   catagory             = "misc";
   shapeFile            = "chromeTest.dts";
   alwaysAmbient = true;
   emap = true;
};
datablock StaticShapeData(starFieldBox)
{
   catagory             = "misc";
   shapeFile            = "starFieldBox.dts";
   alwaysAmbient = true;
};
datablock StaticShapeData(starBox)
{
   catagory             = "misc";
   shapeFile            = "starBoxField.dts";
   alwaysAmbient = true;
};
datablock TriggerData(spaceTrig){
   tickPeriodMS = 32;
};

function spaceTrig::onEnterTrigger(%data, %trigger, %player){
   %pos = %player.getPosition();
   %z = getWord(%pos,2);
   if(!%player.blockTrig){
      %player.blockTrig = 1;
      if(%z > 0){
         %offSetPos = vectorAdd(%pos,"0 0 -622.7");
         %player.setTransform(%offSetPos SPC getWords(%player.getTransform(), 3, 6));
      }
      else{
         %offSetPos = vectorAdd(%pos,"0 0 622.7");
         %player.setTransform(%offSetPos SPC getWords(%player.getTransform(), 3, 6));
      } 
   }
   else{
      %player.blockTrig = 0;
   }
}

function spaceTrig::onleaveTrigger(%data, %trigger, %player){
   
}

function spaceTrig::onTickTrigger(%data, %trig){
   return;
}


datablock ParticleData(StarFieldParticle) {
   dragCoefficient = "0";
   windCoefficient = "0.0";
   gravityCoefficient = "0";
   inheritedVelFactor = "0";
   constantAcceleration = "0";
   lifetimeMS = "9000";
   lifetimeVarianceMS = "1000";
   spinSpeed = "1";
   spinRandomMin = "-50";
   spinRandomMax = "50";
   useInvAlpha = "0";
   textureName = "special/blasterHit";
   colors[0] = "1 1 1 0.0";
   colors[1] = "1 1 1 1.0";
   colors[2] = "1 1 1 1.0";
   colors[3] = "0 0 1 0.25";
   sizes[0] = "0.25";
   sizes[1] = "0.25";
   sizes[2] = "0.25";
   sizes[3] = "0.25";
   times[0] = "0.1";
   times[1] = "0.2";
   times[2] = "0.9";
   times[3] = "1";
};

datablock ParticleEmitterData(StarFieldEmitter) {
   ejectionPeriodMS = "2";
   periodVarianceMS = "0";
   ejectionVelocity = "1";
   velocityVariance = "0";
   ejectionOffset = "120";
   thetaMin = "0";
   thetaMax = "180";
   phiReferenceVel = "0";
   phiVariance = "360";
   overrideAdvance = "1";
   orientParticles = "0";
   orientOnVelocity = "1";
   particles = "StarFieldParticle";
   lifetimeMS = "0";
   lifetimeVarianceMS = "0";
   reverseOrder = "0";
   alignParticles = "0";
};

datablock ParticleData(dustcloud)
{
   textureName = "smoke02";
   lifetimeMS = 8000;
   lifetimeVaranceMS = 100;
   colors[0] = "0.843137 0.729412 0.466667 0";
   colors[1] = "0.843137 0.729412 0.466667 0.55";
   colors[2] = "0.843137 0.729412 0.466667 0.25";
   colors[3] = "0.843137 0.729412 0.466667 0";
   sizes[0] = 10.0;
   sizes[1] = 13.0;
   sizes[2] = 16.0;
   sizes[3] = 10.0;
   times[0] = 0.0;
   times[1] = 0.3;
   times[2] = 0.6;
   times[3] = 1;
   spinSpeed = 1;
   spinRandomMin = -5;
   spinRandomMax = 5;
   gravityCoefficient = 0;
   constantAcceleration = 0;
   dragCoefficient = 0;
   useInvAlpha = true;
   windCoefficient = 1;
};

datablock ParticleEmitterData(dustcloudemitter)
{
   particles = dustcloud;
   ejectionPeriodMS = 100;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 1;
   thetaMax = 90;
   thetaMin = 90;
   phiReferenceVel = 0;
   phiVariance = 360;
   lifeTimeMS = 0;//0
   ejectionOffset = "16";
   overrideAdvance = "0";
   orientParticles = "0";
   orientOnVelocity = "0";
};