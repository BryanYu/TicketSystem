<template>
  <div class="dropdown mb-4">
    <button
      class="btn btn-primary dropdown-toggle"
      type="button"
      id="dropdownMenuButton"
      data-toggle="dropdown"
      aria-haspopup="true"
      aria-expanded="false">
         {{ selected }}
    </button>

    <div class="dropdown-menu animated--fade-in" aria-labelledby="dropdownMenuButton">
      <div>
        <button class="dropdown-item" v-for="item in agents" :key="item.agentCode" @click="clicked(item.agentCode,item.agentName)" >
          {{ item.agentCode }} : {{ item.agentName }}
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "AgentDrowDown",
  props:['modelValue'],
  emits: ['update:modelValue'],
  data() {
    return {
      agents: [],
      selected: ''
    };
  },
  methods: {
    getAgents() {
      this.$axios
        .get(this.$constant.api.getAgents)
        .then((response) => {
          this.agents = response.data;
        })
        .catch(function (err) {
          alert(err);
        });
    },

    clicked(agentCode,agentName) 
    {
        this.selected = `${agentCode}:${agentName}`;
        this.$emit('update:modelValue', agentCode);
    }
  },
  mounted() {
    this.getAgents();
    this.selected = '請選擇平台'
    
  },
};
</script>