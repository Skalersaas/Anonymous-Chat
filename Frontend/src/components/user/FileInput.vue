<script>
const files = ref([]);
const showAttachments = ref(false);
const isImage = (file) => {
return file.type.startsWith("image/");
}
const isVideo = (file) => {
	return file.type.startsWith("video/");
}
const removeFile = (index) => {
	files.value.splice(index, 1);
}

const handleFileUpload = (event) =>
{
	const selectedFiles = Array.from(event.target.files);
    console.log(selectedFiles.length);
    selectedFiles.forEach((file) => {
		const fileReader = new FileReader();
		fileReader.onload = (e) => {
			files.value.push({
				type: file.type,
				preview: e.target.result,
				content: URL.createObjectURL(file),
			});
		};
		fileReader.readAsDataURL(file);
	});
    event.target.value = null;
}
const toggleFileInput = () => {
    document.getElementById('fileInput').click();
}
export { files, showAttachments, isImage, isVideo, removeFile };
</script>
<template>
    <input 
        type="file"
        class="hidden"
        id="fileInput"
        accept="image/*,video/*"
        @change="handleFileUpload"
        multiple>
    
	<button
		type="button"
	  	@click="toggleFileInput"
		:disabled="disabled()"
		class="text-white px-4 py-2 rounded-lg focus:outline-none focus:ring-2 absolute right-12">
		<i class="fa-solid fa-paperclip text-[#dedede]"></i>
    </button>
</template>

<script setup>
    import { ref } from 'vue';
    import { disabled } from './Input.vue';
</script>