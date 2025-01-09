<script>

</script>
<template>
	<div v-if="showAttachments" class="absolute inset-0 bg-gray-200 bg-opacity-50 backdrop-blur-md flex flex-col justify-center items-center z-10">
		<div>
			<label class="block text-gray-700 text-sm mb-1 ">Attach File</label>
			<input
			type="file"
			@change="handleFileUpload"
			multiple
			accept="image/*,video/*"
			class="border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500">
			<button
				@click="showAttachments=false"
				class="bg-blue-500 text-white ml-2 px-4 py-2 rounded-lg hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500">
				Ok!
			</button>
		</div>
		<div v-if="files.length > 0" class="relative p-4 border border-gray-300 rounded-lg space-y-4 overflow-y-auto max-h-fit">
			<div 
				v-for="(file, index) in files" 
				:key="index" 
				class="relative p-2 border border-gray-300 rounded-lg inline-block">
				
				<!-- Image Preview -->
				<img 
					v-if="isImage(file)" 
					:src="file.preview" 
					class="w-48 h-48 object-cover rounded-lg" 
					alt="Preview">
				
				<!-- Video Preview -->
				<video 
					v-else-if="isVideo(file)" 
					:src="file.preview" 
					class="w-48 h-48 object-cover rounded-lg" 
					controls>
				</video>


				<!-- Remove Button -->
				<button 
					@click.prevent="removeFile(index)" 
					class="absolute top-2 right-2 bg-red-500 text-white rounded-full p-1 text-xs hover:bg-red-600">
					✕
				</button>
			</div>
		</div>
	</div>
</template>

<script setup>
import { ref } from 'vue';
</script>