import { Box, Button, ButtonGroup, Grid } from '@mui/material';
import React from 'react';
import { styled } from '@mui/system';
import Container from '@mui/material';
import laJuana from '../../assets/img/logo.png'
import CardIcon from '../../components/cards/CardIcon';
import { useGetListProgramQuery } from '../../store/services/AccessProgram';
interface HomeProps {

}
const AvatarDisplay = styled('img')(({ theme }) => ({
    [theme.breakpoints.down('sm')]: {
        width: "100%"
    },
}));

const Home: React.FunctionComponent<HomeProps> = () => {
    const { data: accessProgramData, error, isLoading } = useGetListProgramQuery();
    return <Box
        paddingTop={5}
        display="flex"
        justifyContent="center"
    >
        <Grid container maxWidth="md" spacing={2}>
            
            {accessProgramData&&
                accessProgramData?.map(access=>{
                    return <Grid item xs={4} key={access.id}>
                        <CardIcon accessProgram={access}/>
                    </Grid>
                })
            }
            
        </Grid>
        
    </Box>
}

export default Home;
