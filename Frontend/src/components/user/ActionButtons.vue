<script>
const menuOpen = ref(false);
const hoveredId = ref(0);
const content = ref("");
const sendCode = (actionType) => {
    const config = {
        code: actionType
    }
    config.profile = (actionType === actionCodes.Start) ? profile.value: '';
    send(config)
}
const toggleProfile = () => {
    profileReady.value = false;
}

const closeMenuOnClickOutside = (event) => 
{
    const menu = document.getElementById('menu'); // Access the menu element via ref
    if (menu && !menu.contains(event.target)) 
    {
        menuOpen.value = false; // Close the menu if clicked outside
    }   
}
document.addEventListener('click', closeMenuOnClickOutside);

const toggleMenu = () => {
    menuOpen.value = !menuOpen.value;
}
const hoverStatusEnter = (id) => {
    hoveredId.value = id;
    switch (id)
    {
        case 1: content.value = "Profile"; break;
        case 2: content.value = "Find"; break;
        case 3: content.value = "Stop"; break;
        case 4: content.value = "Next"; break;
    }
}
</script>


<template>
<div id="menu" class="absolute right-5 cursor-pointer text-lg" @click="toggleMenu()">
    <i class="fa-solid fa-ellipsis-vertical text-white"></i>
</div>

<div v-if="menuOpen" class="absolute flex flex-col right-2 top-14 gap-2">
    <p v-if="hoveredId != 0" 
        class="absolute right-10 bg-[#1b1b1b85] px-2 py-1 rounded-md rounded-tr-none backdrop-blur-lg text-white"
        :style="{ top: (4 + 44 * (hoveredId - 1)) + 'px' }">
        {{ content }}
    </p>

    <button
        :disabled="state == states.paired || state == states.waiting"
        @click="toggleProfile"
        @mouseenter="hoverStatusEnter(1)"
        @mouseleave="hoveredId = 0"
        :class="[
            'actionButton border border-actionProfile text-actionProfile hover:bg-actionProfile hover:text-white',
            state === states.paired || state === states.waiting
            ? 'disabled' : ''
        ]">
       <i class="fa-solid fa-user" al></i>
    </button>
    <button
        :disabled="state == states.paired || state == states.waiting"
        @click="sendCode(actionCodes.Start)" 
          @mouseenter="hoverStatusEnter(2)"
          @mouseleave="hoveredId = 0"
          :class="[
            'actionButton border border-green-500 text-green-500 hover:bg-green-500 hover:text-white',
            state === states.paired || state === states.waiting
            ? 'disabled' : ''
        ]">
      <i class="fa-solid fa-play ms-[2px]"></i>
    </button>   
    <button
    :disabled="state == states.notPaired || state == states.waiting"
    @click="sendCode(actionCodes.Stop)" 
      @mouseenter="hoverStatusEnter(3)"
      @mouseleave="hoveredId = 0"
      :class="[
            'actionButton border border-red-500 text-red-500 hover:bg-red-500 hover:text-white',
            state === states.notPaired || state === states.waiting
            ? 'disabled' : ''
        ]">
      <i class="fa-solid fa-pause "></i>
    </button>    
    <button
        :disabled="state == states.notPaired || state == states.waiting"
        @click="sendCode(actionCodes.Next)" 
          @mouseenter="hoverStatusEnter(4)"
          @mouseleave="hoveredId = 0"
        :class="[
            'actionButton border border-blue-600 text-blue-600 hover:bg-blue-600 hover:text-white',
            state === states.notPaired || state === states.waiting
            ? 'disabled' : ''
        ]">
      <i class="fa-solid fa-forward-step ms-[2px]"></i>
    </button>
</div>


</template>


<script setup>
import { state, states, actionCodes } from '@/scripts/backend';
import { send } from '@/scripts/sender';
import { profile } from "@/components/user/Profile.vue";
import { profileReady } from '@/views/ChatView.vue';
import { ref } from 'vue';
</script>
<style>
    .actionButton {
        @apply bg-white w-9 h-9 rounded-full focus:outline-none;
    }
    .disabled {
        @apply bg-gray-200 border-none text-white cursor-not-allowed hover:bg-gray-200 hover:text-white;
    }
</style>