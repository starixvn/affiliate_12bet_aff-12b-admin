import React from 'react';

import { Col, FormGroup, Label, Input, Form } from 'reactstrap';
import { formatDecimal } from '../../../../../modules/numberFormat';
import { commissionValueList } from '../../../modules/constant';

export default class SharePercentSettingItem extends React.Component {
    constructor(props) {
        super(props);
    }

    handleChangeComm = (event) => {
        const { name, value } = event.target;
        this.props.onChangeCommission({ [name]: Number(value) });
    }

    render() {
        const { label, data, isCreating } = this.props;
        return (
            <FormGroup row>
                <Label sm={4} className="font-weight-bold">{label}</Label>

                {
                    isCreating ? (
                        <Col sm={2}>
                            <Input type="select" name="DeductedPercentage" className="form-control" onChange={this.handleChangeComm}
                                defaultValue={formatDecimal(data && data.DeductedPercentage)}>
                                {
                                    commissionValueList.map((item, i) => {
                                        return (<option key={`share-percent-${i}`} value={item}>{item}</option>);
                                    })
                                }
                            </Input>
                        </Col>
                    ) :
                        (
                            <Col sm={5}>
                                <Form inline>
                                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                                        <Label className="mr-sm-2">Current: {formatDecimal((data && data.DeductedPercentage) || 0)} | Next month: </Label>
                                    </FormGroup>
                                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                                        <Input type="select" name="NextDeductedPercentage" className="form-control" 
                                            defaultValue={formatDecimal((data && data.NextDeductedPercentage) || 0)}
                                            onChange={this.handleChangeComm} style={{ width: 60 }}>
                                            {
                                                commissionValueList.map((item, i) => {
                                                    return (<option key={`share-percent-nextmonth-${i}`} value={item}>{item}</option>);
                                                })
                                            }
                                        </Input>
                                    </FormGroup>
                                </Form>
                            </Col>
                        )
                }
            </FormGroup>
        )
    }
}