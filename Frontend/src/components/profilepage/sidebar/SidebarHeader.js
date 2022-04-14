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
  const [showFollowButton, setShowFollowButton] = useState(false);
  useEffect(() => {
    const accountOnPage = user.username;
    let currentUser = {};
    authService.getCurrentUser().then((res) => {
      currentUser = res.username;
      setShowFollowButton(currentUser !== accountOnPage);
    })
  });

  return (
    <div>
      <div className="item">
        <WhiteTextTypography variant="h5">
          {user?.displayName}
        </WhiteTextTypography>
        <WhiteTextTypography variant="h6">
          {user?.username}
        </WhiteTextTypography>
        <Divider />
        <WhiteTextTypography>{user?.role}</WhiteTextTypography>
        <Divider />

        <img
          src={user?.profilePicture}
          className="profilePicture"
          alt="avatar"
        />

        <Typography>{user?.bio}</Typography>

        {showFollowButton === true ? (
          <FollowButton userDetails={user} />
        ) : null}
      </div>
    </div>
  );
}
