import React, { PureComponent } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Col } from 'reactstrap';

import InputPopUp from "../../../../common/components/InputPopUp";
import { updateContactNumber, getAffiliateInfo } from '../../actions';
import  NotificationPopup  from "../../../../common/components/NotificationPopup";

export class ContactNumber extends PureComponent{
    constructor(props){
        super(props);
        this.state = {
            isOpen : false
        }
    }

    _renderStaticContent = () => {
        const { className, sm, md } = this.props;
        return (
            <Col className={className} sm={sm} md={md}>{this.props.children}</Col>
        )
    }

    _handleOnClick = () => {
        this.setState(
            {
                isOpen : true,
                notificationMessage: "test"
            });
    }

    _handleCloseNotification = () => {
        this.setState({notificationMessage: ""});
    }

    _renderClickableContent = () => {
        const { className, sm, md } = this.props;
        const { isOpen, notificationMessage } = this.state;
        return(
            <Col className={className} sm={sm} md={md}>
                <Button color="link" className="left-panel-btn-link" onClick={this._handleOnClick}>{this.props.children}</Button>
                <InputPopUp isOpen={isOpen} title="Contact number" value={this.props.children}/>
                <NotificationPopup message={notificationMessage} onClose={this._handleCloseNotification}/>
            </Col>
        )
    }

    render(){
        const { isStatic } = this.props;
        const renderContent = isStatic ? this._renderStaticContent : this._renderClickableContent;
        return (
             renderContent()
        );
    }
}

function mapStateToProps({ affCommonInfo, leftPanel }) {
    const { affiliateInfo } = leftPanel;
    const { affName } = affCommonInfo;
    return Object.assign({}, { affiliateInfo, affName });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateInfo, updateContactNumber }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(ContactNumber);