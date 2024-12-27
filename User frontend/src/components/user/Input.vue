<script>
const content = ref("");
const sendMessage = async () => {
	const msg =  {
		code: actionCodes.Transfer,
		content: content.value,
		files: files.value !== '[]' ? await uploadFiles(files.value) : [],
	}
	send(msg);
	message(messageTypes.Outgoing, content.value, files.value);
	content.value = "";
	files.value = [];
	focus();
}
const focus = () => {
	document.getElementById("inpu").focus();
}
export { focus };
</script>


<template>
<Attachments v-if="files.length != 0"/>

<form 
  @submit.prevent="sendMessage()"
  class="border-t border-gray-300 p-4 flex flex-col">
  
  <!-- Text Input -->
<div class="flex items-center space-x-2 relative">
    <input
        id="inpu"
        :required="files.length == 0"
        type="text" 
        v-model="content"
        :disabled="state != states.paired"
        placeholder="Type your message..." 
		class="flex-1 rounded-2xl px-4 pr-12 py-2 bg-[#f9f9f9] focus:outline-none focus:ring-2 focus:ring-blue-500">


	<button
		type="button"
	  	@click="showAttachments = true"
		:disabled="state != states.paired"
		class="text-white px-4 py-2 rounded-lg focus:outline-none focus:ring-2 absolute right-12">
		<i class="fa-solid fa-paperclip text-[#dedede]"></i>
	<!-- {{ files.length == 0 ? <FontAwesomeIcon :icon="byPrefixAndName.fas['house']" /> : files.length + "file(s)"}} -->
    </button>


	<button
		type="submit"
		:disabled="state != states.paired"
		class="bg-blue-500 text-white w-9 h-9 rounded-full hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500">
		<i class="fa-solid fa-location-arrow fa-rotate-by ms-[-3px]" style="--fa-rotate-angle: 45deg;"></i>
    </button>


</div>
  <!-- File Previews -->
<FileAttachments v-if="showAttachments"/>
</form>

</template>

<script setup>
import { ref } from 'vue';
import { actionCodes, state, states, uploadFiles } from '@/scripts/backend';
import { message, messageTypes } from '@/views/ChatView.vue';
import FileAttachments, { files, showAttachments } from './FileAttachments.vue';
import { send } from '@/scripts/sender';
import Attachments from './AttachmentsPreview.vue';
</script>
