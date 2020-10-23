import React, { PureComponent } from "react";
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Col } from 'reactstrap';

import InputPopUp from "../../../../common/components/InputPopUp";
import { updateContactNumber, getAffiliateInfo, toggleLoaderLeftPanel } from '../../actions';
import  NotificationPopup  from "../../../../common/components/NotificationPopup";
import { messages } from '../../modules/constants';

export class ContactNumber extends PureComponent{
    constructor(props){
        super(props);
        this.state = {
            isOpen : false,
            notificationMessage: ""
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
            });
    }

    _handleCloseNotification = () => {
        this.setState({notificationMessage: ""});
    }

    _toogle = () => {
        this.setState({isOpen: !this.state.isOpen});
    }

    _handleSubmit = async (value) => {
        this._toogle();
        const param = Object.assign({}, {AffId: this.props.affiliateInfo.AffId, ContactNumber: value })
        toggleLoaderLeftPanel(true);
        var result = await this.props.updateContactNumber(param);
        toggleLoaderLeftPanel(false);   
        if (result.IsSuccessful) {
            this.setState({ notificationMessage: messages.contactNumberUpdatedSuccess});
            this.props.getAffiliateInfo(this.props.affName);
        }
        else {
            this.setState({ notificationMessage: result.Message});
        }
    } 

    _renderClickableContent = () => {
        const { className, sm, md } = this.props;
        const { isOpen, notificationMessage } = this.state;
        return(
            <Col className={className} sm={sm} md={md}>
                <Button color="link" className="left-panel-btn-link" onClick={this._handleOnClick}>{this.props.children}</Button>
                <InputPopUp isOpen={isOpen} title="Contact number" value={this.props.children} toggle={this._toogle} handleSubmit={this._handleSubmit}/>
                <NotificationPopup message={notificationMessage} onClose={this._handleCloseNotification} isConfirmPopup={false} />
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
    console.log(affiliateInfo);
    const { affName } = affCommonInfo;
    return Object.assign({}, { affiliateInfo, affName });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getAffiliateInfo, updateContactNumber, toggleLoaderLeftPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(ContactNumber);