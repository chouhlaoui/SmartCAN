# SmartCan Project

## Overview

SmartCan is an innovative IoT project that brings intelligence to waste management. This project comprises an application layer, designed to enhance traditional waste bins into intelligent devices. SmartCan includes two primary components: Admin and User. The Admin section is responsible for managing employees, overseeing waste bins, and ensuring user access. The User section allows users to explore a map displaying the locations of various waste bins, check fill levels, receive real-time notifications, plan routes to specific bins, view notification history, and personalize their information through a profile page.

## Features

### Admin Section

- **Employee Management:**
  - Admin can manage employee accounts.
  - Approval process for new employee accounts.

- **Waste Bin Management:**
  - Admin has control over waste bins.

### User and Admin Section

- **Map Integration:**
  - Users and Admins can view the locations of different waste bins on a map, powered by OpenStreetMap.

- **Fill Status:**
  - Users and Admins can check the fill status of waste bins.

- **Real-time Notifications:**
  - Users and Admins receive real-time notifications on location changes and fill levels.

- **Google Maps Integration:**
  - Users and Admins can plan a route to a specific waste bin using Google Maps.

- **Notification History:**
  - Users and Admins can access a list of previous notifications.

- **Profile Management:**
  - Users and Admins can modify their personal information through a profile page.

## Firebase Configuration

To use this project, configure your Firebase database with the following structure:

### Users

- **Nombre_users (Number of Users):** digit

- **Normal:**
  - **id:**
    - **ID:** User ID
    - **Email:** User Email
    - **Nom:** User Name
    - **Approved:** User approval status (boolean)
    - **NotApproved:** User disapproval status (boolean)
    - **Tel:** User Telephone
    - **mdp:** User Password

  - **admin:**
    - **ID:** 0 (Fixed ID for Admin)
    - **Email:** Admin Email
    - **Nom:** Admin Name
    - **Tel:** Admin Telephone
    - **mdp:** Admin Password

### Carte (Map)

- **Nombre_poubelle (Number of Bins):** digit

- **Poubelle+id:**
  - **Altitude:** float (Bin Altitude)
  - **Longitude:** float (Bin Longitude)
  - **Latitude:** float (Bin Latitude)
  - **Ultrason:** float (Ultrasonic sensor data for the bin)

## How to Configure Firebase

1. **Create a Firebase Project:**
   - Visit the [Firebase Console](https://console.firebase.google.com/).
   - Click on "Add Project" and follow the setup instructions.

2. **Enable Firebase Authentication:**
   - In the Firebase Console, navigate to the "Authentication" section.
   - Enable the sign-in method (email/password) for user authentication.

3. **Set Up Realtime Database:**
   - In the Firebase Console, navigate to the "Database" section.
   - Create a new Realtime Database.
   - Use the provided structure to set up the database.

4. **Security Rules:**
   - Update Firebase security rules to ensure proper access control based on your project requirements.

5. **Firebase Configuration in Project:**
   - Update your SmartCan project configuration to include the Firebase SDK and connection details.

## Example Firebase Configuration (JavaScript):

```javascript
// Initialize Firebase
var firebaseConfig = {
  apiKey: "YOUR_API_KEY",
  authDomain: "your-project-id.firebaseapp.com",
  databaseURL: "https://your-project-id.firebaseio.com",
  projectId: "your-project-id",
  storageBucket: "your-project-id.appspot.com",
  messagingSenderId: "your-messaging-sender-id",
  appId: "your-app-id",
};

// Initialize Firebase
firebase.initializeApp(firebaseConfig);
