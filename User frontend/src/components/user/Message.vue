<script setup>
import { messageTypes } from '@/views/ChatView.vue';

defineProps({
    MessageType: Number,
    Content: String, // For text messages
    MediaFiles: Array, // Array of File objects
});
</script>

<template>
<div 
    :class="[ 
        'flex items-start',
        MessageType === messageTypes.Outgoing ? 'justify-end' : 
        MessageType === messageTypes.System  || MessageType === messageTypes.Profile ? 'justify-center' : ''
    ]">
    <div
        :class="[ 
            'px-4 py-2 rounded-lg max-w-xs space-y-2',
            MessageType === messageTypes.Incoming ? 'bg-gray-200 text-gray-800' :
            MessageType === messageTypes.Outgoing ? 'bg-blue-500 text-white text-right' :
            'bg-green-500 text-white text-center' 
        ]">
        <!-- Text Content -->
        <template v-if="Content">
            <p class="h-auto break-words">{{ Content }}</p>
        </template>
        
        <!-- Media Files -->
        <template v-for="(file, index) in MediaFiles" :key="index">
            <!-- Image File -->
            <template v-if="file.type.startsWith('image/')">
                <img 
                    :src="file.preview" 
                    alt="Image" 
                    class="w-full h-auto rounded-lg shadow-sm">
            </template>
            
            <!-- Video File -->
            <template v-else-if="file.type.startsWith('video/')">
                <video 
                    :src="file.preview" 
                    controls 
                    class="w-full h-auto rounded-lg shadow-sm">
                </video>
            </template>
        </template>
    </div>
</div>
</template>
