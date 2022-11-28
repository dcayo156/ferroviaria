import { Navigate, Outlet, useLocation } from 'react-router-dom';
import * as React from 'react';
import Box from '@mui/material/Box';

import Toolbar from '@mui/material/Toolbar';
import { Container, useTheme } from '@mui/material';
import MainAppBar from './AppBar';
import MainDrawer from './Drawer';
import { useAuth } from '../../hook/useAuth';
import { useTokenHasExpired } from '../../hook/useTokenHasExpired';

const drawerwidth = 215;

export default function Index() {
    const auth = useAuth();
    const tokenExpired=useTokenHasExpired();
    const location = useLocation();
    const width= window.screen.width;
    const theme=useTheme();
    const [open, setOpen] = React.useState(theme.breakpoints.values.sm < width);
    const handleDrawer = () => {
        setOpen(!open);
    };
    return (!tokenExpired && auth.user != null) ?(
        <>
            <Box sx={{ display: 'flex' }}>
                <MainAppBar open={open} drawerwidth={drawerwidth} switchOpen={handleDrawer}/>
                <MainDrawer open={open} drawerwidth={drawerwidth} switchOpen={handleDrawer} />
                <Box
                    component="main"
                    sx={{
                        backgroundColor: (theme) =>
                            theme.palette.mode === 'light'
                                ? theme.palette.grey[100]
                                : theme.palette.grey[900],
                        flexGrow: 1,
                        height: '100vh',
                        overflow: 'auto',
                    }}
                >
                    <Toolbar />
                    <Container maxWidth="xl" sx={{ mt: 2, mb: 2 }}>
                        <Outlet/>
                    </Container>
                </Box>

            </Box>
        </>
    ):(
        <Navigate to="/authentication/login" state={{ from: location }} />
      );
}