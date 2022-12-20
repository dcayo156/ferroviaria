import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import { styled } from '@mui/material/styles';
import logo from '../../assets/img/logo.png'
import { useAuth } from '../../hook/useAuth';
import { Navigate, Outlet, useLocation } from 'react-router-dom';
import { useTokenHasExpired } from '../../hook/useTokenHasExpired';
const AppBarDisplay = styled('div')(({ theme }) => ({
    [theme.breakpoints.down('md')]: { //cuando es chico
        display: 'none',
    },
}));
const ImageDisplay = styled('div')(({ theme }) => ({
    [theme.breakpoints.up('md')]: { //cuando es grande 
        display: 'none',
    },
    [theme.breakpoints.down('md')]: {
        backgroundColor: theme.palette.primary.main
    },
}));
export default function Header() {
    const auth = useAuth();
    const location = useLocation();
    const tokenHasExpired=useTokenHasExpired();
    return (!tokenHasExpired && auth.user != undefined) ? (
        <Navigate to="/main/home" state={{ from: location }} />
    ) : (
        <>
            <header style={{ textAlign: "center" }}>
                <AppBarDisplay>
                    <AppBar position="relative">
                        <Toolbar>
                            <img src={logo} style={{ "width": "4em" }} />
                            <Typography variant="h1" color="black">
                                Ferroviaria Oriental
                            </Typography>
                        </Toolbar>
                    </AppBar>
                </AppBarDisplay>
                <ImageDisplay >
                    <img src={logo} style={{ "width": "50%" }} />
                </ImageDisplay>
            </header>
            <Outlet />
        </>
    );
}