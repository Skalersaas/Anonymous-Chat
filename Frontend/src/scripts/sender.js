import { socket } from "./backend";

const chunkSize = 1024 * 4 - 100;

const send = (data) => 
{
    const json = JSON.stringify(data);
    const chunks = [];
    for (let start = 0; start < json.length;)
    {
        chunks.push(json.slice(start, start + chunkSize));
        start += chunkSize;
    }

    chunks.forEach((chunk, index) => {
        const msg = JSON.stringify({
            part: index + 1,
            totalParts: chunks.length,
            data: chunk
        })
        socket.send(msg);
    })
}

export { send }