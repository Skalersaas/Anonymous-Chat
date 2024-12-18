import { message, messageTypes, resetCompanion, setCompanion } from "@/views/ChatView.vue";
import { ref } from "vue";
const api = "://localhost:7131/";
const socket = new WebSocket('wss' + api + "chatting/connect");

const actionCodes = {
    Transfer: 0,
    Start: 1,
    Next: 2,
    Waiting: 3,
    Stop: 4,
    Profile: 5
}
const states = {
    notPaired: 0,
    waiting: 1,
    paired: 2
}

const state = ref(states.notPaired);

const start = () => {
    socket.addEventListener("open", () => {
        console.log('[ OPEN ] - socket opened')
        message(messageTypes.System, "You're connected! Press on three dots to find pair!!");
    });
    socket.addEventListener("message", (event) => {
        console.log('GELDI')
        parse(event.data)
    });
    socket.addEventListener("close", (event) => {
        console.log('[ CLOSE ] - socket closed')
    });
};
const parse = (data) => {
    data = JSON.parse(data);
    switch (data.type)
    {
        case actionCodes.Transfer: message(messageTypes.Incoming, data.content, data.files); break;
        case actionCodes.Start: message(messageTypes.System, "Pair found!!"); setCompanion(data.profile); state.value = states.paired; break;
        case actionCodes.Waiting: message(messageTypes.System, "Waiting for pair!"); state.value = states.waiting; break;
        case actionCodes.Stop: message(messageTypes.System, "Talk ended!"); resetCompanion(); state.value = states.notPaired; break;
    }
}

window.addEventListener("unload", () => {
    if (socket.readyState === WebSocket.OPEN) {
        socket.close(1000, "Client left")
    }
});


const getAdminData = async () => {
    const request = await fetch('https' + api + 'admin/data', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    })
    if (request.ok)
    {
        const json = await request.json();
        return json;
    }
}


export { socket, actionCodes, state, states, start, getAdminData }