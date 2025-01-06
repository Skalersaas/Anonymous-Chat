<script>
</script>

<template>
    <div class="flex m-auto justify-center gap-4">
        <template v-for="(theme, id) in themes">
            <div 
                :style="{ backgroundColor: theme }"
                class="w-6 h-6  rounded-full border cursor-pointer"
                :class="(id + 1) === activeTheme ? 'border-white' : 'border-[#eaae68]'"
                @click="changeTheme(id + 1)">
            </div>
        </template>
    </div>
</template>
<script setup>
import { ref } from 'vue';
    const themes = ['#8a9455', '#a4a4a2', '#7c8397'];
    const activeTheme = ref(1);
    const changeTheme = (id) => {
        document.documentElement.setAttribute("data-theme", 'theme-' + id);
        activeTheme.value = id;
    }
</script>