<template>
    <ul class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" id="accordionSidebar">
        <!-- Sidebar - Brand -->
        <a class="sidebar-brand d-flex align-items-center justify-content-center">
            <div class="sidebar-brand-icon rotate-n-15">
                <i class="fas fa-laugh-wink"></i>
            </div>
            <div class="sidebar-brand-text mx-3"> Ticket System </div>
        </a>

        <!-- Divider -->
        <hr class="sidebar-divider my-0">

        <li class="nav-item">
            <router-link class="nav-link" to="/Domain">
                <i class="fas fa-fw fa-chart-area"></i>
                <span>修改Domain</span>
            </router-link>
        </li>

        <li class="nav-item">
            <router-link class="nav-link" to="/Counter">
                <i class="fas fa-fw fa-table"></i>
                <span>修改App版號</span>
            </router-link>
        </li>

        <li class="nav-item">
            <a class="nav-link">
                <i class="fas fa-fw fa-table"></i>
                <span>財務補單</span>
            </a>
        </li>
       
    </ul>
</template>

<style>

</style>
<script>
    export default {
        name: "SideBar"
    }
</script>