<script>
const gender = ref();
const genders = {
	male: 1,
	female: 2,
	other: 3
}
const pseudonym = ref("");

const profile = ref ({
	gender: gender,
	pseudonym: pseudonym
})
const saveProfile = () => {
	profileReady.value = true;
	sessionStorage.setItem("profile", JSON.stringify({
	  pseudonym: pseudonym.value,
	  gender: gender.value
	}))
}
const data = JSON.parse(sessionStorage.getItem('profile'));
if (data) {
	gender.value = data.gender;
	pseudonym.value = data.pseudonym;
}

export { profile };
</script>

<template>
  <div class="bg-white p-8 rounded-xl shadow-md w-full max-w-lg">
	<h1 class="text-3xl font-semibold mb-6 text-center text-gray-800">Welcome to Anonymous Chat!</h1>
	<p class="text-center text-gray-600 mb-6">
	  Please fill out the form to create your profile.
	</p>
	<form @submit.prevent="saveProfile()" id="profileForm" class="space-y-6">
	  <!-- Pseudonym -->
	  <div>
		<label for="pseudonym" class="block text-sm font-medium text-gray-700 mb-1">Pseudonym</label>
		<input
		  v-model="pseudonym"
		  type="text"
		  id="pseudonym"
		  name="pseudonym"
		  class="block w-full rounded-lg border border-gray-300 py-2 px-4 shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-400 sm:text-sm"
		  placeholder="Enter your pseudonym"
		  required
		/>
	  </div>

	  <!-- Gender -->
	  <div>
		<label for="gender" class="block text-sm font-medium text-gray-700 mb-1">Gender</label>
		<select
		  v-model="gender"
		  id="gender"
		  name="gender"
		  class="block w-full rounded-lg border border-gray-300 py-2 px-4 shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-400 sm:text-sm"
		  required
		>
		  <option value="" disabled selected>Select your gender</option>
		  <option :value="genders.male">Male</option>
		  <option :value="genders.female">Female</option>
		  <option :value="genders.other">Other</option>
		</select>
	  </div>

	  <!-- Submit Button -->
		<button
			type="submit"
			class="w-full bg-blue-500 text-white py-3 px-6 rounded-lg shadow-md hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-400">
		  	Submit Profile
		</button>
	</form>
  </div>
</template>

<script setup>
import { profileReady } from '@/views/ChatView.vue';
import { onMounted, ref } from 'vue';
import { state, states } from '@/scripts/backend';

onMounted(() => {
	document.getElementById("pseudonym").focus()
})
</script>