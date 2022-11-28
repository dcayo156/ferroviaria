import { Avatar, Card, CardActions, CardContent, CardHeader, CardMedia, Collapse, IconButton, Typography } from '@mui/material';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import * as React from 'react';
import { red } from '@mui/material/colors';
import EditIcon from '@mui/icons-material/Edit';
import ShareIcon from '@mui/icons-material/Share';
import { useGetFindPeopleByIdQuery } from '../../store/services/Person'
import { Gender } from '../../store/types/Person'
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom'
interface ProfileProps {

}

const Profile: React.FunctionComponent<ProfileProps> = () => {
    const { id }: any = useParams();
    const navigate = useNavigate();
    const { data, error, isLoading } = useGetFindPeopleByIdQuery(id);
    const onClickEditPerson = () => {
        navigate(`/person/${id}/edit`)
    }
    return (<Card sx={{ maxWidth: 345 }}>

        <CardHeader
            avatar={
                <Avatar sx={{ bgcolor: red[500] }} aria-label="recipe">
                    R
                </Avatar>
            }
            action={
                <IconButton aria-label="settings">
                    <MoreVertIcon />
                </IconButton>
            }
            title={`${data?.firstName} ${data?.secondName} ${data?.lastName} `}
            subheader={data?.birthDate}
        />


        <CardContent>
            <Typography variant="body2" color="text.secondary">
                This impressive paella is a perfect party dish and a fun meal to cook
                together with your guests. Add 1 cup of frozen peas along with the mussels,
                if you like.
            </Typography>
        </CardContent>
        <CardActions disableSpacing>
            <IconButton aria-label="add to favorites" onClick={onClickEditPerson}>
                <EditIcon />
            </IconButton>
            <IconButton aria-label="share">
                <ShareIcon />
            </IconButton>
        </CardActions>

    </Card>);
}

export default Profile;