import { Avatar, IconButton, Toolbar, Typography } from '@mui/material';
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import { styled, useTheme, Theme, CSSObject } from '@mui/material/styles';
import MenuIcon from '@mui/icons-material/Menu';
import * as React from 'react';
import ProfileMenu from './ProfileMenu';
import logo from '../../assets/img/logo.png'
interface AppBarProps {
    open?: boolean;
    drawerwidth: number
}

const AppBar = styled(MuiAppBar, {
    shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({ theme, open, drawerwidth }) => ({
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    ...(open && {
        marginLeft: drawerwidth,
        width: `calc(100% - ${drawerwidth}px)`,
        transition: theme.transitions.create(['width', 'margin'], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen,
        }),
    }),
}));
interface MainAppBarProps {
    open?: boolean,
    drawerwidth: number,
    switchOpen: (event: React.MouseEvent<HTMLElement>) => void
}
const MainAppBar: React.FunctionComponent<MainAppBarProps> = ({ open, drawerwidth, switchOpen }) => {
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const handleClickMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };
    return (<AppBar position="fixed" open={open} drawerwidth={drawerwidth}>
        <Toolbar>
            <IconButton
                aria-label="open drawer"
                onClick={switchOpen}
                edge="start"
                sx={{
                    marginRight: 5,
                    ...(open && { display: 'none' }),
                }}
            >
                <MenuIcon />
            </IconButton>
            <img src={logo} style={{ "width": "4em" }} />
            <Typography variant="h1" color="black" >
                Ferroviaria Oriental
            </Typography>
            <IconButton
                onClick={handleClickMenu}
                size="small"
                edge="end"
                sx={{ ml: 2, position: "absolute", top: "10px", right: "20px" }}
            >
                <Avatar sx={{ width: 32, height: 32 }}></Avatar>
            </IconButton>
            <ProfileMenu anchorEl={anchorEl} setAnchorEl={setAnchorEl} />
        </Toolbar>
    </AppBar>);
}

export default MainAppBar;