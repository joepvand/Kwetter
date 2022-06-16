import React, {useEffect} from "react";
import { Divider, Typography } from "@material-ui/core";
import { withStyles } from "@material-ui/core/styles";
import "./SidebarHeader.css";
import FollowButton from "./followButton/FollowButton";
import authService from "../../../services/auth.service";
import { useState } from "react";

const WhiteTextTypography = withStyles({
  root: {
    color: "#FFFFFF",
  },
})(Typography);

export default function SidebarHeader({ user }) {

  return (
    <div>
      <div className="item">
        <WhiteTextTypography variant="h5">
          {user?.displayName}
        </WhiteTextTypography>
        <Divider />


        <img
          src={"data:image/png;base64," + user?.profilePictureBase64}
          className="profilePicture"
          alt="avatar"
        />

        <Typography>{user?.biography}</Typography>
      </div>
    </div>
  );
}
