The pet project uses different methods of controlling from 10K to 90K objects on the screen. The objects are a grass model animated using PerlinNoise with the ability to “touch” it. The project allows you to evaluate the difference in FPS with different implementation methods and different numbers of objects on the screen. The methods are arranged from less optimal to more optimal:

-each GameObject has its own separate instance of the MonoBehaviour controlling script;

-objects on the scene are controlled by a single manager;

-use of batching (the scene was removed from the build, but remained in the project because after implementation via ECS, batching works by default);

-GPU instancing;

-GPU instancing + jobs + burst compile;

-DOTS (ECS + jobs + burst compile).

Build: https://drive.google.com/file/d/1OZ8Ym1ovOwHNeIOdjuouihOwfTQBLl7U/view?usp=sharing

