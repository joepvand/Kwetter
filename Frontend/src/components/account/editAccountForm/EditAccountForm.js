import { Button } from '@material-ui/core';
import React from 'react'
import { useEffect } from 'react';
import { useState } from 'react'
import userService from '../../../services/user.service';
import EditDetailsForm from '../editDetailsForm/EditDetailsForm';
import ProfilePictureEditor from '../editProfilePicture/ProfilePictureEditor'
import { useAlert } from 'react-alert'
import "./editAccountForm.css"
export default function EditAccountForm({accountDetails}) {
    const [profilePictureData, setProfilePictureData] = useState("");
    const [details, setDetails] = useState({});
    const alert = useAlert();
    useEffect(()=> {
        setProfilePictureData(accountDetails.profilePicture)
    },[accountDetails])

    const saveChanges = () => {
        details.profilePicture = profilePictureData;

        console.log(details);
                userService.updateUserDetails(details)
                .then((reply) => {alert.success("Profile is updates, refresh to view changes")})
                .catch(err => alert.error(err.response.data.message));
    }
    return (
        <div id="formContainer">
            <img src={profilePictureData} id="profilePicture" alt="profile"/>
            <ProfilePictureEditor value={profilePictureData} callback={setProfilePictureData}/>
            <EditDetailsForm accountDetails={accountDetails} callback={setDetails}/>

            <Button onClick={saveChanges}>Save changes</Button>
        </div>
    )
}
