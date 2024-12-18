<template>
<div class="border-t border-gray-300 p-2">
    <div class="flex gap-4 overflow-x-auto scrollbar-thin scrollbar-thumb-gray-400 scrollbar-track-gray-200 w-full">
        <div 
            v-for="(file, index) in files" 
            :key="index" 
            class="relative p-2 border border-gray-300 rounded-lg flex-shrink-0 w-16">
            
            <img 
                v-if="isImage(file)" 
                :src="file.preview" 
                class="object-cover rounded-lg" 
                alt="Preview">
            
            <video 
                v-else-if="isVideo(file)" 
                :src="file.preview" 
                class="w-12 h-12 object-cover rounded-lg" 
                controls>
            </video>

            <button 
                @click.prevent="removeFile(index)" 
                class="absolute top-1 right-1 bg-red-500 text-white rounded-full w-4 h-4 text-xs hover:bg-red-600">
                ✕
            </button>
        </div>
    </div>
</div>


</template>

<script setup>
import { files } from './FileAttachments.vue';
import { isVideo, isImage, removeFile } from './FileAttachments.vue';
console.log(files.value.length)
</script>