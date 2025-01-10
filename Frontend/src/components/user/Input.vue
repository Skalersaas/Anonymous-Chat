<script>
const content = ref("");
const sendMessage = async () => {
	const tmp = {
		content: content.value,
		files: files.value
	}
	message(messageTypes.Outgoing, content.value, files.value);
	content.value = "";
	files.value = [];

	send(await createMsg(tmp));
	focus();
}
const createMsg = async (tmp) => {
	const msg =  {
		code: actionCodes.Transfer,
		content: tmp.content,
		files: tmp.files !== '[]' ? await uploadFiles(tmp.files) : [],
	}
	return msg;
}
const focus = () => {
	document.getElementById("inpu").focus();
}

const disabled = () =>{
	return state.value != states.paired;
}
export { disabled, focus };
</script>


<template>
<AttachmentsPreview v-if="files.length != 0"/>

<form 
  @submit.prevent="sendMessage()"
  class="border-t border-gray-300 p-4 flex flex-col bg-secondary">
  
  <!-- Text Input -->
<div class="flex items-center space-x-2 relative">
    <input
        id="inpu"
        :required="files.length == 0"
        type="text" 
        v-model="content"
        :disabled="disabled()"
        :placeholder="disabled() ? 'Find pair first!!' : 'Type your message...'" 
		class="text-textInput border border-inputBorder  flex-1 rounded-2xl px-4 pr-12 py-2 focus:outline-none
	    bg-inpuEnabled">

	<FileInput/>



	<button
		type="submit"
		:disabled="disabled()"
		class="bg-sendBg text-white w-9 h-9 rounded-full enabled:hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500">
		<i class="fa-solid fa-location-arrow fa-rotate-by ms-[-3px] rotate-45"></i>
    </button>


</div>
</form>

</template>

<script setup>
import { ref } from 'vue';
import { actionCodes, state, states, uploadFiles } from '@/scripts/backend';
import { message, messageTypes } from '@/views/ChatView.vue';
import { send } from '@/scripts/sender';
import AttachmentsPreview from './AttachmentsPreview.vue';
import FileInput, { files }  from './FileInput.vue';
</script>
