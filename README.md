# Far-Flung: A Psyche Mission Adventure

[![Build](https://github.com/cboveda/psyche-game/actions/workflows/main.yml/badge.svg)](https://github.com/cboveda/psyche-game/actions/workflows/main.yml)
[![codecov](https://codecov.io/gh/cboveda/psyche-game/branch/dev/graph/badge.svg?token=VODBNEAIG3)](https://codecov.io/gh/cboveda/psyche-game)

## Playable Demo

- [cboveda.github.io/psyche-game/](https://cboveda.github.io/psyche-game/)

## The Game

- **Far-Flung** consists of a series of five mini-games. Each mini-game takes the player through one aspect of the Psyche mission: assembly of the orbitter itself, launching the orbitter to the asteroid Psyche, collecting data from the surface of the asteroid, communicating that data back to Earth, and finally analyzing the data. Each game offers a variety of different player experiences, and new ways to encourage the player to learn more about the Psyche mission.
- The games are tied together by the Hub. From the Hub, players can choose to either play through a story-driven campaign through the mini-games, or they can explore each of the mini-games in a 'free play' mode. In the campaign mode, the games are linked together through cinematic experiences and engaging dialog. 

### Hub

![hub](/img/demo1.gif)

### Cinematic Transitions

![launch](/img/demo3.gif)

### Assembly Game

![assembly](/img/demo2.gif)

### Flightpath Game

![flightpath](/img/demo4.gif)

### Scanning Game

![scanning](/img/demo5.gif)

### Communications Game

![communications](/img/demo6.gif)

### Lab Game

![lab](/img/demo7.gif)

## How to Build

1) Open project in Unity Editor and confirm editor version in Unity Hub (See [#Development Tools](#development-tools))  
   ![1](/img/1.png)
2) File > Build Settings   
   ![2](/img/2.png)
3) Build and player settings are saved as part of the project assets, and no modification is required. Click 'Build' and choose an output path.  
   ![3](/img/3.png)  
4) After the build is complete, confirm the contents of the output.  
   ![4](/img/4.png)  
5) The native resolution of the game is **1440 x 900**. Do **not** change the 'height' and 'width' attributes of the canvas element. However, the style of the HTML canvas element can be adjusted to accomodate different different screensizes or website layouts by changing the height and width properties of the style attribute. Be sure that the height and width match a 1.6:1 ratio (e.g. 960 x 600, see below).  
   ![5](/img/5.png)  
6) The scripts necessary to load the build in a web browser will not work when loaded from local storage. You may host the files on a simple static server for testing. Unity provides web server tool as part of its default installation, and it can be found here: `Unity\Editor\Data\PlaybackEngines\WebGLSupport\BuildTools\SimpleWebServer.exe`

## Credits

### Contributors

- Marcus Maczynski
- Ian Swanlund
- Jason Supnet
- Chris Boveda
- Adam Carroll

## Development Tools

-  [Unity Editor v2020.3.18f1](https://unity3d.com/unity/qa/lts-releases)
-  [Git LFS v2.13.3](https://git-lfs.github.com/)
-  [VSCode v1.60.1](https://code.visualstudio.com/)

### Third Party Assets

-  [SimplySpace Interiors - Cartoon Assets](https://assetstore.unity.com/packages/3d/environments/sci-fi/simple-space-interiors-cartoon-assets-87964): Using the Multi-Entity license type under the [Using Standard Unity Asset Store EULA](https://unity3d.com/legal/as_terms?_gl=1*19fell3*_gcl_aw*R0NMLjE2MzQ0MDI3NzcuQ2p3S0NBanc4S21MQmhCOEVpd0FRYnFOb0lmX3VidkZTMmtSS3AxY25BMUFacUdjRl83NmRraEhyTjJlYi1pS2F1aS03U3lfV3c3MFNSb0NVRU1RQXZEX0J3RQ..&_ga=2.107343298.1186192561.1634328229-1057728314.1629309491&_gac=1.61557726.1634402777.CjwKCAjw8KmLBhB8EiwAQbqNoIf_ubvFS2kRKp1cnA1AZqGcF_76dkhHrN2eb-iKaui-7Sy_Ww70SRoCUEMQAvD_BwE) | Cost: $47.97
-  [SimplySpace - Cartoon Assets](https://assetstore.unity.com/packages/3d/vehicles/space/simple-space-cartoon-assets-82496): Using the Multi-Entity license type under the [Using Standard Unity Asset Store EULA](https://unity3d.com/legal/as_terms?_gl=1*19fell3*_gcl_aw*R0NMLjE2MzQ0MDI3NzcuQ2p3S0NBanc4S21MQmhCOEVpd0FRYnFOb0lmX3VidkZTMmtSS3AxY25BMUFacUdjRl83NmRraEhyTjJlYi1pS2F1aS03U3lfV3c3MFNSb0NVRU1RQXZEX0J3RQ..&_ga=2.107343298.1186192561.1634328229-1057728314.1629309491&_gac=1.61557726.1634402777.CjwKCAjw8KmLBhB8EiwAQbqNoIf_ubvFS2kRKp1cnA1AZqGcF_76dkhHrN2eb-iKaui-7Sy_Ww70SRoCUEMQAvD_BwE) | Cost: $35.97
-  [SimplySpace - Cartoon Assets](https://syntystore.com/products/polygon-office-pack): Using the Synty Store EULA license type [as per the Synty EULA](https://syntystore.com/pages/end-user-licence-agreement) | Cost: $39.99

### Music
  
- Arcadia - Kevin MacLeod
- Tranquility - Kevin MacLeod
  
### Sound Effects

- Button Push - Mike Koenig
- Click On - Mike Koenig
- Button Click Off - Mike Koenig
- Woosh - Mark DiAngelo
- Beep Ping - Mike Koenig
- Mouse Double Click - SoundBible.com

## Disclaimer

This work was created in partial fulfillment of Arizona State University Capstone Course “SER401/402″. The work is a result of the Psyche Student Collaborations component of NASA’s Psyche Mission (https://psyche.asu.edu). “Psyche: A Journey to a Metal World” [Contract number NNM16AA09C] is part of the NASA Discovery Program mission to solar system targets. Trade names and trademarks of ASU and NASA are used in this work for identification only. Their usage does not constitute an official endorsement, either expressed or implied, by Arizona State University or National Aeronautics and Space Administration. The content is solely the responsibility of the authors and does not necessarily represent the official views of ASU or NASA.


