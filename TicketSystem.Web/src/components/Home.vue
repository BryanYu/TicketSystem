<template>
    <div id="wrapper">
        <side-bar></side-bar>
        <div id="content-wrapper" class="d-flex flex-column">
            <div id="content">
                <top-bar></top-bar>
                <div class="container-fluid">
                    <router-view></router-view>
                </div>
            </div>
             <footer class="sticky-footer bg-white">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span>Copyright &copy; Ticket System 2021</span>
                    </div>
                </div>
            </footer>
        </div>
    </div>
</template>


<script>
import SideBar from '@/components/SideBar.vue'
import TopBar from '@/components/TopBar.vue'
import constant from '../common/constant';

export default {
  name: 'Layout',
  components: {
      SideBar,
      TopBar
  },
  mounted() {
      var token = sessionStorage.getItem(constant.token);
      if(token === null || token === '')
      {
          this.$router.push('/Login');
      }
  }
}
</script>

