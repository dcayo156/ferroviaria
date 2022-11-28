import { Box, Button, ButtonGroup } from '@mui/material';
import React from 'react';
import { styled } from '@mui/system';
import laJuana from '../../assets/img/logo.png'
interface HomeProps {

}
const AvatarDisplay = styled('img')(({ theme }) => ({
    [theme.breakpoints.down('sm')]: {
        width: "100%"
    },
}));

const Home: React.FunctionComponent<HomeProps> = () => {
    return <Box
        display="flex"
        flexDirection="column"
    >
        <Box
            display="flex"
            justifyContent="center"
        >
            <AvatarDisplay src={laJuana} />
        </Box>
        <Box
            display="flex"
            justifyContent="center"
        >
            <ButtonGroup disableElevation variant="contained">
                <Button>Personas</Button>
                <Button>Busquedas</Button>
            </ButtonGroup>
        </Box>
    </Box>
}

export default Home;
