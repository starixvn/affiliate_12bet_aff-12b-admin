import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { FormGroup, Label, Col, Input } from 'reactstrap';

import { setDealInfo } from '../../actions';
import { htmlDecode } from '../../../../../modules/htmlHelper';

const MAX_LENGTH = 100;

export const DealInfo = (props) => {

    const { DealName, DealType, Description, dealTypeList } = props;

    const _handleChangeText = (e) => {
        const { name, value } = e.target;
        if (value.length <= MAX_LENGTH) {
            props.setDealInfo({ [name]: value });
        }
    }

    const _handleChangeDealType = (e) => {
        const { value } = e.target;
        props.setDealInfo({ DealType: value });
    }

    return (
        <div>
            <FormGroup row>
                <Label htmlFor="DealName" sm={2}>Deal Name <span className="text-danger required" style={{ margin: 0 }}>*</span> </Label>
                <Col sm={8}>
                    <Input type="text" name="DealName" id="DealName" value={htmlDecode(DealName)} onChange={_handleChangeText} />
                </Col>
                <Col sm={2}>
                </Col>
            </FormGroup>
            <FormGroup row>
                <Label htmlFor="DealType" sm={2}>Deal Types <span className="text-danger required" style={{ margin: 0 }}>*</span> </Label>
                <Col sm={8}>
                    <Input type="select"
                        className="mr-sm-2 search-box"
                        value={DealType}
                        onChange={_handleChangeDealType}>
                        {Object.keys(dealTypeList).map((key, index) => (
                            <option key={index} value={key}>{dealTypeList[key]}</option>
                        ))}
                    </Input>
                </Col>
                <Col sm={2}>
                </Col>
            </FormGroup>
            <FormGroup row>
                <Label htmlFor="Description" sm={2}>Description </Label>
                <Col sm={8}>
                    <Input type="text" name="Description" id="Description" value={htmlDecode(Description)} onChange={_handleChangeText} />
                </Col>
                <Col sm={2}>
                </Col>
            </FormGroup>
        </div>
    )
}

const mapStateToProps = ({ dealInfo, dealTypeList }) => {
    const { DealName, DealType, Description } = dealInfo;
    return {
        DealName,
        DealType,
        Description,
        dealTypeList
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setDealInfo
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(DealInfo));