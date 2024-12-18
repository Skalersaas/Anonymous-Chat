<script>
const users = ref([])
const pairs = ref([])
const  updateData = async () => {
  	const dat = await getAdminData();
  	users.value = dat.users;
  	pairs.value = dat.pairs;
}
</script>

<template>
  <div class="container h-[100vh] mx-auto">
    <!-- Navbar -->
    <nav class="bg-white shadow-md  py-4 px-6 container mx-auto">
        <h1 class="text-xl font-bold text-gray-800">Admin Panel</h1>
        <button @click="updateData()">Update</button>
    </nav>

    <!-- Section Content -->
    <section class="py-10 flex space-x-10">
        <Users :users="users"/>
        <Pairs :pairs="pairs"/>
    </section>
  </div>
</template>

<script setup>
import { onMounted, ref } from 'vue';
import Users from '@/components/admin/Users.vue';
import Pairs from '@/components/admin/Pairs.vue';
import { getAdminData } from '@/scripts/backend';
onMounted( async () => {
  await updateData();
})
</script>