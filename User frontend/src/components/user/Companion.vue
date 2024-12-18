<script setup>
defineProps({
    Pseudonym: String,
    Gender: Number
})

</script>

<template>
<div class="flex items-center w-full justify-center">
    <template v-if="Pseudonym">
        <p :class="['text-end',
            Gender == 3? 'w-full':'w-1/2 '
        ]">{{ Pseudonym }}</p>
       <div class="flex items-center w-1/2">
        <span v-if="Gender < 3" class="w-2 h-2 bg-gray-300 rounded-full mx-2"></span>
        <p v-if="Gender < 3">{{ Gender == 1 ? "Man" : "Woman" }}</p>
       </div>
    </template>
    <template v-else>
        No companion
    </template>
</div>

</template>