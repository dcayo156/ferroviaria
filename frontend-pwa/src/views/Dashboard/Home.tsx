import { Box, Button, ButtonGroup, Grid } from '@mui/material';
import React from 'react';
import { styled } from '@mui/system';
import Container from '@mui/material';
import laJuana from '../../assets/img/logo.png'
import CardIcon from '../../components/cards/CardIcon';
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
    >
        <Grid container spacing={2}>
            <Grid item xs={4}>
                <CardIcon></CardIcon>
            </Grid>
            <Grid item xs={4}>
                <CardIcon></CardIcon>
            </Grid>
            <Grid item xs={4}>
                <CardIcon></CardIcon>
            </Grid>
            <Grid item xs={4}>
                <CardIcon></CardIcon>
            </Grid>
            <Grid item xs={4}>
                <CardIcon></CardIcon>
            </Grid>
            <Grid item xs={4}>
                <CardIcon></CardIcon>
            </Grid>
        </Grid>
        
    </Box>
}

export default Home;
