# Real-Time Anonymous Chat Application

## Description
This is a real-time chat application built with an **ASP.NET** backend and a **Vue.js** frontend. The application enables users to connect with random pairs for live conversations via WebSockets. Users can share files, photos, and videos during their chats and switch to the next pair if desired. Each user has a profile containing basic information such as their pseudonym and gender.

---

## Features

### Core Functionalities
- **Real-Time Communication:**
  - WebSocket-based communication for instant text and media sharing.
  - Seamless connection and message delivery.

- **Random Pairing:**
  - Users can be matched randomly with other online users.
  - Option to switch to the next available pair for a new chat.

- **File Sharing:**
  - Support for sharing photos, videos, and other files during a conversation.

### User Profiles
- **Basic Information:**
  - Pseudonym
  - Gender
- Profiles are visible during conversations to provide minimal context.

### Intuitive Interface
- User-friendly design for smooth navigation.
- Fast and responsive experience with the Vue.js frontend.


### Admin Panel
- All users and pairs are visible here
- No authentification yet
---

## Technologies Used

### Backend
- **ASP.NET Core**
  - Handles WebSocket connections and real-time data transfer.

### Frontend
- **Vue.js**
  - Provides a reactive and dynamic user interface.
  - Allows seamless transitions between chat pairs.

---

## How It Works
1. **Profile Setup:**
   - Users provide a pseudonym and gender when entering the app.

2. **Pair Matching:**
   - The system matches users randomly via WebSocket connections.

3. **Real-Time Chat:**
   - Users can communicate in real-time with their pair, sharing text and files.

4. **Switch Pairs:**
   - If desired, users can disconnect and switch to the next available pair.

5. **Profiles:**
   - Users can view minimal profile data (pseudonym and gender) of their chat pair.

---

## Installation and Setup

### Prerequisites
- **Node.js** (for Vue.js frontend)
- **.NET SDK** (for ASP.NET backend)

### Steps
1. **Clone the repository:**
   ```
   git clone https://github.com/Skalersaas/Anonymous-Chat.git
   cd Anonymous-Chat
   ```

2. **Backend Setup:**
   - Navigate to the backend folder.
   - Install dependencies and run the server:
     ```
     dotnet restore
     dotnet run
     ```

3. **Frontend Setup:**
   - Navigate to the frontend folder.
   - Install dependencies and start the development server:
     ```
     npm install
     npm run dev
     ```

4. **Run the Application:**
   - Access the frontend at `http://localhost:3000` (or specified port).
   - Ensure the backend is running on its designated port `localhost:7131`.

---

## Usage Instructions
1. Open the application and provide your pseudonym and gender.
2. Get matched with a random user for real-time chat.
3. Share text, photos, videos, or other files with your pair.
4. Switch to the next pair whenever you like.
5. Enjoy seamless, real-time communication!

---

## Contribution
Feel free to contribute to the project by:
- Reporting issues
- Suggesting new features
- Submitting pull requests