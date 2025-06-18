import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { FormGroup, Label, Col, Input } from 'reactstrap';

import { setDealInfo } from '../../actions';
import { htmlDecode } from '../../../../../modules/htmlHelper';
import { DEAL_CPA } from '../../libs/constants';
import { buildDefaultRevenueShare } from '../../libs/revenue-share-service';

const MAX_LENGTH = 100;

export const DealInfo = (props) => {

    const { GroupId, DealName, DealType, Description, RevenueTierSetting, dealTypeList } = props;

    const _handleChangeText = (e) => {
        const { name, value } = e.target;
        if (value.length <= MAX_LENGTH) {
            props.setDealInfo({ [name]: value });
        }
    }

    const _handleChangeDealType = (e) => {
        const { value } = e.target;
        var dealType = Number(value);

        if (dealType != DEAL_CPA && !RevenueTierSetting) {
            const { deductionList, productList } = props;
            const revenueShareInfo = buildDefaultRevenueShare(deductionList, productList);

            props.setDealInfo({ DealType: dealType, ...revenueShareInfo });
        } else {
            props.setDealInfo({ DealType: dealType });
        }
    }

    return (
        <div>
            <FormGroup row className='align-items-center'>
                <Label htmlFor="DealName" sm={2}>Deal Name <span className="text-danger required" style={{ margin: 0 }}>*</span> </Label>
                <Col sm={8}>
                    <Input type="text"
                        name="DealName"
                        id="DealName"
                        value={htmlDecode(DealName)}
                        onChange={_handleChangeText} />
                </Col>
                <Col sm={2}>
                </Col>
            </FormGroup>
            <FormGroup row className='align-items-center'>
                <Label htmlFor="DealType" sm={2}>Deal Types <span className="text-danger required" style={{ margin: 0 }}>*</span> </Label>
                <Col sm={8}>
                    <Input type="select"
                        className="mr-sm-2 search-box"
                        name="DealType"
                        id="DealType"
                        disabled={!!GroupId && GroupId > 0}
                        value={DealType}
                        onChange={_handleChangeDealType}>
                        {dealTypeList.map((item, index) => (
                            <option key={index} value={item.GroupTypeId}>{item.GroupTypeName}</option>
                        ))}
                    </Input>
                </Col>
                <Col sm={2}>
                </Col>
            </FormGroup>
            <FormGroup row className='align-items-center'>
                <Label htmlFor="Description" sm={2}>Description </Label>
                <Col sm={8}>
                    <Input type="text"
                        name="Description"
                        id="Description"
                        value={htmlDecode(Description)}
                        onChange={_handleChangeText} />
                </Col>
                <Col sm={2}>
                </Col>
            </FormGroup>
        </div>
    )
}

const mapStateToProps = ({ dealInfo, dealTypeList, deductionList, productList }) => {
    const { GroupId, DealName, DealType, Description, RevenueTierSetting } = dealInfo;
    return {
        GroupId,
        DealName,
        DealType,
        Description,
        RevenueTierSetting,
        dealTypeList,
        deductionList,
        productList
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setDealInfo
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(DealInfo));