<template>
    <ul class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" id="accordionSidebar">
        <!-- Sidebar - Brand -->
        <a class="sidebar-brand d-flex align-items-center justify-content-center">
            <div class="sidebar-brand-icon rotate-n-15">
                <i class="fas fa-laugh-wink"></i>
            </div>
            
                <div class="sidebar-brand-text mx-3"> 
                    Ticket System 
                </div>
            
        </a>

        <!-- Divider -->
        <hr class="sidebar-divider my-0">

        <li class="nav-item">
            <router-link class="nav-link" to="/Ticket">
                <i class="fas fa-fw fa-chart-area"></i>
                <span>Ticket List</span>
            </router-link>
        </li>
    </ul>
</template>

<style>

</style>
<script>
    export default {
    }
</script>