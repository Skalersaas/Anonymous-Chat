<script>
const content = ref("");
const files = ref([]);
const sendMessage = () => {
    
const json = JSON.stringify(
    {
        content: content.value,
        code: 0,
        file: files.value[0].preview
    }
)
send(json);
content.value = ''
}
const handleFileUpload = (event) =>
{
	const selectedFiles = Array.from(event.target.files);
	selectedFiles.forEach((file) => {
		const fileReader = new FileReader();
		fileReader.onload = (e) => {
			files.value.push({
				blob: file,
				preview: e.target.result,
			});
		};
		fileReader.readAsDataURL(file);
	});
}
</script>

<template>
<form
    @submit.prevent="sendMessage"
    class="flex justify-center items-center relative h-screen w-[30%] m-auto">
    <input
        id="inpu"
        required
        type="text" 
        v-model="content"
        placeholder="Type your message..." 
		class="flex-1 border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500">
	<input 
        type="file"
        @change="handleFileUpload">
    <button
		type="submit"
		class="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500">
      	Send
    </button>
</form>
</template>
<script setup>
import { ref } from 'vue';
import { socket } from '@/scripts/backend';
import { send } from '@/scripts/sender';
</script>