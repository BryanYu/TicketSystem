<template>
    <nav class="navbar navbar-expand navbar-light bg-white topbar mb-4 static-top shadow">
                    <!-- Topbar Navbar -->
                    <ul class="navbar-nav ml-auto">
                        <!-- Nav Item - User Information -->
                        <li class="nav-item dropdown no-arrow">
                            <a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button"
                                data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <span class="mr-2 d-none d-lg-inline text-gray-600 small">Welcome {{ account }}, Role:{{ roleType }}</span>
                            </a>
                            <!-- Dropdown - User Information -->
                            <div class="dropdown-menu dropdown-menu-right shadow animated--grow-in"
                                aria-labelledby="userDropdown">
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item" href="#" data-toggle="modal" data-target="#logoutModal">
                                    <i class="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-gray-400"></i>
                                    <a href="#" @click="logout">
                                        Logout
                                    </a>
                                    
                                </a>
                            </div>
                        </li>
                    </ul>
                </nav>
</template>

<script>
import constant from '../common/constant';
import dataService from '../services/dataService';
export default {
    data() {
        return {
            account:'',
            roleType:''
        }
    },
    methods: {
        getAccountInfo() {
            dataService.getAccountInfo()
            .then(result => {
                this.account = result.data.data.account;
                this.roleType = result.data.data.roleType;
                sessionStorage.setItem(constant.sesstionStorageKey.permissions, result.data.data.permissions);
                sessionStorage.setItem(constant.sesstionStorageKey.ticketTypes, result.data.data.ticketTypes);
            })
        },
        logout() {
            
            dataService.logout()
            .then(result => {
                if(result.status === 200 && result.data.code === 0) {
                    sessionStorage.removeItem(constant.sesstionStorageKey.token);
                    sessionStorage.removeItem(constant.sesstionStorageKey.permissions);
                    sessionStorage.removeItem(constant.sesstionStorageKey.ticketTypes);
                    this.$router.push('/Login');
                }
            })
            
        }
    },
    mounted() {
        this.getAccountInfo();
    }
}

</script>
