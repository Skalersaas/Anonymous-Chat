import { socket } from "./backend";

const send = (data) => 
{
    socket.send(JSON.stringify(data));
}

export { send }