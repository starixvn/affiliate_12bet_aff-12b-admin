import React, { useState, useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Alert, FormGroup, Button, Col } from 'reactstrap';

import { setDealInfo } from '../../actions';
import { buildDefaultDealInfo } from '../../libs/service';

import DealInfo from './../shared/DealInfo';
import SubAffRateSetting from './../shared/sub-aff-rate';
import { DealTypeInfo } from './../shared/DealTypeInfo';

export const AddDeal = (props) => {

    const [isShowMessage, setIsShowMessage] = useState(false);
    const [message, setMessage] = useState([]);
    const [isSuccess, setIsSuccess] = useState(true);

    useEffect(() => {
        const dealInfo = buildDefaultDealInfo();

        props.setDealInfo(dealInfo);
    }, []);

    const _handleSave = () => {
        const { dealInfo } = props;
        
        props.onSave();
    }

    return props.dealInfo && (
        <React.Fragment>
            <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                <div dangerouslySetInnerHTML={{ __html: message.join('<br>') }}></div>
            </Alert>
            <DealInfo />
            <SubAffRateSetting />
            <DealTypeInfo />
            <p className="text-danger my-2">Note: Change will take effect on current month (GMT-4)</p>
            <FormGroup row inline className="align-items-center">
                <Col sm={1}>
                    <Button type="button" className="my-2 btn btn-success btn-save" onClick={_handleSave}>SAVE</Button>
                </Col>
                <Col sm={1}>
                    <Button type="button" className="my-2 btn btn-info" onClick={props.onClose}>CANCEL</Button>
                </Col>
            </FormGroup>
        </React.Fragment>
    )
}

const mapStateToProps = ({ dealInfo }) => {
    return {
        dealInfo
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setDealInfo
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AddDeal));