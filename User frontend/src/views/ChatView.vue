<script>
const profileReady = ref(false);
const messages = ref([]);
const companion = ref({
	gender: -1,
	pseudonym: ''
});

const messageTypes = {
    Incoming: 0,
    Outgoing: 1,
	System: 2,
}
const message = async (type, content, files) => {
	let media = [];
	if (files != null && files.length != 0)
	{
		if (type == messageTypes.Incoming)
			media = await downloadFiles(files);
		else 
			media = files;
	}
	messages.value.push(
		{
			type: type,
			content: content,
			mediaFiles: media
		}
	);
	const container = document.getElementById("messageContainer");
	if (container)
	{
		setTimeout(() => {
        	container.scrollTop = container.scrollHeight;
		}, 1);
		focus();
	}
}

const setCompanion = (comp) => {
	companion.value = comp;
}
const resetCompanion = () => {
	companion.value = {};
}
export { messageTypes, profileReady, resetCompanion, setCompanion, message}
</script>


<template>
<body class="bg-gray-100 h-screen flex justify-center items-center relative">
	<div v-if="!profileReady" class="absolute inset-0 bg-gray-200 bg-opacity-50 backdrop-blur-md flex justify-center items-center z-10">
		<Profile/>
	</div>
	<div class="bg-white shadow-md rounded-lg w-full max-w-lg flex flex-col h-[90vh] relative">
	<!-- Header -->
	<div class="bg-blue-500 text-white text-center p-4 rounded-t-lg">
		<h1 class="text-lg font-semibold">Anonymous Chat</h1>
	</div>

	<div class="border-b border-gray-300 p-2 flex items-center justify-center relative">
		<Companion :-gender="companion.gender" :-pseudonym="companion.pseudonym"/>
		<ActionButtons/>
	</div>
	
	<!-- Chat Messages -->
	<div
		id="messageContainer" 
		class="flex-1 overflow-y-auto p-4 space-y-4 scroll-smooth">
		<Message
			v-for="m in messages"
			:-MessageType="m.type"
			:-content="m.content"
			:-media-files="m.mediaFiles ?? []"
			/>
	</div>
	<!-- Actions -->
		<!-- <ActionButtons/> -->
	
	<!-- Message Input -->
		<Input/>
	</div>
</body>
</template>

<script setup>
import Message from "@/components/user/Message.vue";
import ActionButtons from "@/components/user/ActionButtons.vue";
import Profile from "@/components/user/Profile.vue";
import { ref } from "vue";
import Input, { focus } from "@/components/user/Input.vue";
import Companion from "@/components/user/Companion.vue";
import { downloadFiles } from "@/scripts/backend";
</script>