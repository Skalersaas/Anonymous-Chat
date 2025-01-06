<script setup>
import { messageTypes } from '@/views/ChatView.vue';

const props = defineProps({
    MessageType: Number,
    Content: String, // For text messages
    MediaFiles: Array, // Array of File objects
});
const getPosition = () => {
    if (props.MessageType === messageTypes.Outgoing)
        return 'justify-end';
    else if (props.MessageType === messageTypes.System)
        return 'justify-center'
}
const getColors = () => {
    if (props.MessageType === messageTypes.Incoming)
        return 'bg-msgBgIn rounded-tl-3xl rounded-lg text-msgTextIn';
    else if (props.MessageType === messageTypes.Outgoing)
        return 'bg-msgBgOut rounded-tr-3xl rounded-lg text-msgTextOut text-right';

    return 'bg-msgBgSys rounded-lg text-msgTextSys text-center border-4 border-msgBorderSys';
}
</script>

<template>
<div 
    :class="[ 
        'flex items-start', getPosition()
    ]">
    <div
        :class="[ 
            'px-4 py-2 max-w-xs space-y-2', getColors()
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
