import { Divider, IconButton, List,Color } from '@mui/material';
import * as React from 'react';
import LinkTo from './ItemTo';
import HomeIcon from '@mui/icons-material/Home';
import UserIcon from '@mui/icons-material/People';
import MapIcon from '@mui/icons-material/Map';
import AutoStoriesIcon from '@mui/icons-material/AutoStories';
import { styled, useTheme, Theme, CSSObject } from '@mui/material/styles';
import MuiDrawer from '@mui/material/Drawer';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import { getCredentials } from '../../store/slices/Auth/localStorage';
interface DrawerProps {
    open?: boolean,
    drawerwidth:number
}
const openedMixin = (theme: Theme,drawerwidth:number): CSSObject => ({
    width: drawerwidth,
    transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
    }),
    overflowX: 'hidden',
    background: theme.palette.secondary.main,
    color:"#fff"
});
const closedMixin = (theme: Theme): CSSObject => ({
    transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: 'hidden',
  width: 0,
    [theme.breakpoints.up('sm')]: {
        width: `calc(${theme.spacing(8)} + 1px)`,
    },
    background: theme.palette.secondary.main,
    color:"#fff",
});
const DrawerHeader = styled('div')(({ theme }) => ({
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'flex-end',
    padding: theme.spacing(0, 1),
    // necessary for content to be below app bar
    ...theme.mixins.toolbar,
}));

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })<DrawerProps>(
    ({ theme, open, drawerwidth }) => ({
        width: drawerwidth,
        flexShrink: 0,
        whiteSpace: 'nowrap',
        boxSizing: 'border-box',
        ...(open && {
            ...openedMixin(theme,drawerwidth),
            '& .MuiDrawer-paper': openedMixin(theme,drawerwidth),
        }),
        ...(!open && {
            ...closedMixin(theme),
            '& .MuiDrawer-paper': closedMixin(theme),
        }),
    }),
);
interface MainDrawerProps {
    open?: boolean,
    drawerwidth:number,
    switchOpen: () => void
}
const MainDrawer: React.FunctionComponent<MainDrawerProps> = ({open,drawerwidth,switchOpen}) => {
    const theme = useTheme();
    const uri:string=window.location.pathname;
    return ( <Drawer variant="permanent" open={open} drawerwidth={drawerwidth}>
    <DrawerHeader>
        <IconButton onClick={()=>switchOpen()}>
            {theme.direction === 'rtl' ? <ChevronRightIcon /> : <ChevronLeftIcon />}
        </IconButton>
    </DrawerHeader>
    <Divider />
    <List>
        <LinkTo to="/main/home" text="Home" open={open} active={uri.includes("/main/home")} switchOpen={switchOpen}>
            <HomeIcon/>
        </LinkTo>
        <LinkTo to="/access-program" text="Acceso a programas" open={open} active={uri.includes("/access-program")} switchOpen={switchOpen}>
            <MapIcon/>
        </LinkTo>
        <LinkTo to="/category" text="Categorias" open={open} active={uri.includes("/category")} switchOpen={switchOpen}>
            <AutoStoriesIcon />
        </LinkTo>
        <LinkTo to="/relationship/relationshipType" text="Document" open={open} active={uri.includes("/relationship/relationshipType")} switchOpen={switchOpen}>
            <UserIcon />
        </LinkTo>
        <LinkTo to="/person" text="Personas" open={open} active={uri.includes("/person")} switchOpen={switchOpen}>
            <UserIcon />
        </LinkTo>
        <LinkTo to="/user" text="Usuarios" open={open} active={uri.includes("/user")} switchOpen={switchOpen}>
            <UserIcon />
        </LinkTo>
    </List>
</Drawer> );
}
 


export default MainDrawer;