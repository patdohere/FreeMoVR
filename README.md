#FreeMoVR: Naturally Mapped Movement for Virtual Reality (VR) Systems#
Project Proposal for University of California, Irvine Senior Design
Last Revised: December 14, 2013 at 5:48 PM

Stark'11 Labs


*Patrick Do, 4th Year Undergraduate, CSE and Physics, Project Lead & Software Engineer*

*Jonathan Lin, 4th Year Undergraduate, Computer Science & Engineering, Software Developer*

*Chynna Velasco, 4th Year Undergraduate, Computer Science & Engineering, Embedded Systems*

*Tanuja Undevalli, 4th Year Undergraduate, Computer Science & Engineering, Hardware Engineer*

Mentors & Advisors

*Mark Bachman, Professor EECS & BME, Faculty Advisor* 

*Fadi Kurdahi, Professor EECS, Faculty Advisor*

##Executive Summary
We are mapping user movement into the virtual world using a natural process rather than the using a joystick or input device that users control with their hands to move around in the virtual world. 


Our project will solve this problem of immersion by actually mapping human foot movement and translating it into a directional movement vector in the (x, y, z) coordinates. This will then be available to use in game engines and other virtual reality world where player movement is important. We will achieve this by using optoelectronic, accelerometers and gyroscope sensors.  The user will wear special shoes that have these sensors embedded onto them. The special shoes will allow us to track each foot movement.  The optoelectronic sensors will act like an optical mouse on each foot so we can measure the x and y movement. The accelerometers will help us find the z movement.  Finally, the gyroscopes will help us find the orientation of the user with respect to a calibrated normal.  In order to make this work, the user's shoes will have a low coefficient of friction towards the surface that they will walk on and will be held in place by a harness.   This is needed so that they don't move physically, but they still feel as if they are moving in the virtual world. 


The project will schedule progress meetings every 2 weeks with our mentor and the project.

##Introduction
In virtual reality space, there is no way to map movement without having the users to use a joystick or input device that they control with their hand to move through the virtual world. The use of joystick or input device reduces the user's experience and immersion with virtual reality. Therefore, we are planning to map user movement into virtual world using a natural process. The use of this product would be anything that needs to track human movement. This project is primarily targeting gaming market where it is important for the user to get ultimate experience while doing their daily activities such as walking. However, this project can be expanded to track the statistics of users which can be helpful in research for health related topics. 

##Background
A completely immersive virtual reality which is fully compatible with real life is becoming closer and closer to reality with the advancement of today's technology.  The entire design of virtual reality is to help bridge the gap between natural human interaction and technology.  It is well known that the benefits of virtual assistance with everyday tasks are limitless.  However, the problem is that technology is often cumbersome, inconvenient, or simply difficult to use.  Virtual reality systems provide a simple and efficient solution to directly interface the user with technology. The market for virtual reality is steadily growing, with the advent of the technology dating back to the 1990's. From the Department of Defense to the hardworking civilian, there are great benefits to be gained from directly interacting with technology through motion and voice. Touch screens and simple voice commands are the norm nowadays, but eventually everything that a user would do on a computer monitor can be done without an actual display medium, 'appearing' directly in the empty space in front of the user.  This sort of technology may still be in development, but it is reasonable to think that the long sought futuristic technology that empowers human users with real-time holographic windows may not be so far-fetched after all.

##Development Plan

###Project Overview
Our projects main focus is to design and build our FreeMoVR USB device to integrate with Microsoft Windows and be recognized by Half-Life VR.  However for the Virtual Reality experience we have other devices that we must define and there purpose before breaking down FreeMoVR.

##Stark'11 Labs Team
###Patrick Do
Strengths: Software programming, Embedded System Design, System Integration 


Responsibilities: Calibration Application, Project Management, Construction of harness and surface  


I am currently a fourth year at UCI majoring in CSE and Physics and am on track to graduating in 2015.  I strongly believe that you can never stop learning.  This motto is essential to my lifestyle. I am in many clubs on campus such as IEEE, ICS Student Council, Imports@UCI, and eSports@UCI.  I had an internship at ViaSat Inc. as a software engineer where I developed internal software tools for the company.  I believe that the combination of CSE and Physics can help me towards a career I will be passionate about.

###Jonathan Lin

Strengths: Software programming, Device Integration, Embedded Systems 


Responsibilities: Device/Driver interface for USB Device, Calibration Application 


I am a fourth-year student who is majoring in computer science engineering at UCI. I have been interested in computers and technology from a very young age, and started experimenting with computer hardware and software when I received my first computer, back in 2004. Some of my past achievements are interning at companies like Northrop Grumman and being awarded fellowship with the Cybercorps: Scholarship for Service. It is now my last year at UCI and I plan to specialize in network security and parallel/distributed systems. I hope to use the abilities and skills I have learned to one day benefit humanity.

###Chynna Velasco

Strengths: Software Programming, Embedded Systems 


Responsibilities: Arduino code for USB Device, Algorithms for Sensor Aggregation 


I’m currently a fifth year student at the University of California, Irvine majoring in Computer Science and Engineering. I transferred from community college on the fall quarter of 2011.That same fall quarter of transferring, I joined the professional co-ed engineering fraternity, Theta Tau. I am currently the webmaster of the Pi Delta Chapter of Theta Tau. On the summer of 2013, I earned an internship with IBM as a software engineering intern. I currently develop a tool called the Database Conversion Workbench (DCW), a migration tool for their product called DB2zOS. From my experiences, I plan to pursue a path in relation to database and web development in hopes of creating useful innovations with the two.

###Tanuja Undevalli

Strengths: Hardware Programming, DSP Sensor 


Responsibilities: Construction of FreeMoVR, Filter and DSP of Sensor Data 


I am a fourth-year student who is majoring in computer science and engineering at University of California, Irvine. I was an undergraduate research assistant last year for Richard H. Lathrop who is the director of ICS Honors Program at UCI. I worked on automating scripts for their central tumor suppressor protein simulation. I have also interned for companies such as NVIDIA since sophomore year and have worked on Kepler and Maxwell GPUs. I plan to use my knowledge attained in college to work on integrated circuit design which includes design verification and validation of IC layout.


