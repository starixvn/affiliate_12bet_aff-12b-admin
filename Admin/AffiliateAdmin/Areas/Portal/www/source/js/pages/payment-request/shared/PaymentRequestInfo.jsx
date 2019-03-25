import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Row, Col, FormGroup, Label, Input } from 'reactstrap';

import { resource, statusList } from '../modules/constants';
import { formatDecimalWithCurrencySymbol } from '../../../modules/numberFormat';

export class PaymentRequestInfo extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { paymentRequestInfo, adminDesc, affDesc, handleChange, handleChecked } = this.props;
        const isPending = paymentRequestInfo.Status.toLowerCase() == 'pending';

        return (
            <React.Fragment>
                <fieldset>
                    <legend>
                        <h6 className="font-weight-bold">{resource.AccountInformation}</h6>
                    </legend>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.UserName}</Label>
                        <Label sm={6}><strong>{paymentRequestInfo.AffName}</strong></Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.RequestedDate}</Label>
                        <Label sm={6}><strong>{paymentRequestInfo.TransDateText}</strong></Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.EmailAddress}</Label>
                        <Label sm={6}>{paymentRequestInfo.Email}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.FirstName}</Label>
                        <Label sm={6}>{paymentRequestInfo.FirstName}</Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.LastName}</Label>
                        <Label sm={6}>{paymentRequestInfo.LastName}</Label>
                    </FormGroup>
                </fieldset>
                <fieldset>
                    <legend>
                        <h6 className="font-weight-bold">{resource.PaymentInformation}</h6>
                    </legend>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.PaymentMethod}</Label>
                        <Label sm={6}><strong>{paymentRequestInfo.PaymentMethod}</strong></Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.Amount}</Label>
                        <Label sm={6}><strong>{formatDecimalWithCurrencySymbol(paymentRequestInfo.Amount)}</strong></Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.Status}</Label>
                        <Label sm={6}><strong>{paymentRequestInfo.Status}</strong></Label>
                    </FormGroup>
                    <FormGroup row className="mb-2">
                        <Label sm={6}>{resource.UserName12Bet}</Label>
                        <Label sm={6}><strong>{paymentRequestInfo.PlayerUsername}</strong></Label>
                    </FormGroup>
                    {
                        isPending ? (
                        <FormGroup row className="mb-2 ml-1">
                            <FormGroup className="mr-4" check>
                                <Label check>
                                    <Input name="status" type="radio" className="mr-2" value="Approved" onChange={(event) => handleChecked(event)} />
                                    {statusList.Approved}
                                </Label>
                            </FormGroup>
                            <FormGroup check>
                                <Label check>
                                    <Input name="status" type="radio" className="mr-2" value="Rejected" onChange={(event) => handleChecked(event)} />
                                    {statusList.Rejected}
                                </Label>
                            </FormGroup>
                        </FormGroup>
                        ) : (
                        <React.Fragment>
                            <FormGroup row className="mb-2">
                                <Label sm={6}>{resource.CompletedDate}</Label>
                                <Label sm={6}><strong>{paymentRequestInfo.CompletedDateText}</strong></Label>
                            </FormGroup>
                            <FormGroup row className="mb-2">
                                <Label sm={6}>{resource.UpdatedBy}</Label>
                                <Label sm={6}>{paymentRequestInfo.Creator}</Label>
                            </FormGroup>
                        </React.Fragment>
                        )
                    }
                </fieldset>
                <Row>
                    <Col sm={6}>
                        <fieldset>
                            <legend>
                                <h6 className="font-weight-bold">{resource.AdminNote}</h6>
                            </legend>
                            <FormGroup className="mb-2">
                                <Label for="adminDesc">{resource.VisibleOnly}</Label>
                                <Input type="textarea" name="adminDesc" rows="8" disabled={!isPending} value={adminDesc} onChange={(event) =>handleChange(event)} />
                            </FormGroup>
                        </fieldset>
                    </Col>
                    <Col sm={6}>
                        <fieldset>
                            <legend>
                                <h6 className="font-weight-bold">{resource.AffiliateNote}</h6>
                            </legend>
                            <FormGroup className="mb-2">
                                <Label for="affDesc">{resource.VisibleAlso}</Label>
                                <Input type="textarea" name="affDesc" rows="8" disabled={!isPending} value={affDesc} onChange={(event) =>handleChange(event)} />
                            </FormGroup>
                        </fieldset>
                    </Col>
                </Row>
            </React.Fragment>
        );
    }
}

function mapStateToProps({ paymentRequestInfo }) {
    return Object.assign({}, { paymentRequestInfo });
}

export default connect(mapStateToProps)(PaymentRequestInfo);