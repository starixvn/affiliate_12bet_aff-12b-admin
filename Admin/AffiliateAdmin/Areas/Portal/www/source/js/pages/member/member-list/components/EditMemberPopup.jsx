    import React, { PureComponent } from 'react';
    import { connect } from 'react-redux';
    import { bindActionCreators } from 'redux';

    import { Button, Form, FormGroup, Col, Label, Input, Modal, ModalHeader, ModalBody, Alert } from 'reactstrap';
    import { DatePicker } from '@9softstudio/react-datepicker';
    import { globalResource } from '../libs/constants';
    import { monthNames } from '../../../../modules/constants';
    import { fetchData, getMemberDetail, updateMember, setSelectedMemberId } from '../actions';

    const initialState = {
        memberName: '',
        affUserName: '',
        lastActionDate: '',
        fromDate: '',
        isClickedSubmit: false,
        isShowMessage: false,
        isSuccess: true,
        message: ''
    };

    export class EditMemberPopup extends PureComponent {
        constructor(props) {
            super(props);

            const { today } = this.props;
            const now = new Date(today);
            const nextMonth = new Date(now.getFullYear(), now.getMonth() + 2, 0);
            this.nextMonthString = `${nextMonth.getMonth() + 1}/${nextMonth.getDate()}/${nextMonth.getFullYear()}`;

            const lastBillingDate = this.getLatestBillingDate(today);
            this.lastBillingDate = lastBillingDate;
            this.lastBillingDateString = `${lastBillingDate.getMonth() + 1}/${lastBillingDate.getDate()}/${lastBillingDate.getFullYear()}`;

            this.state = {
                ...initialState,
                fromDate: `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`
            }
        }

        componentDidUpdate = (prevProps) => {
            const { selectedMemberId } = this.props;

            if (!!selectedMemberId && selectedMemberId !== prevProps.selectedMemberId) {
                this.getMemberInfo(selectedMemberId);
            }
        }

        getMemberInfo = async (selectedMemberId) => {
            var memberInfo = await this.props.getMemberDetail(selectedMemberId);

            if (!!memberInfo) {
                this.setState(prevState => Object.assign({}, prevState, {
                    memberName: memberInfo.UserName,
                    affUserName: memberInfo.AffUserName,
                    lastActionDate: memberInfo.LastAction
                }))
            }
        }

        getLatestBillingDate = (today) => {
            const now = new Date(today);
            const billingDateOfMonth = new Date(now.getFullYear(), now.getMonth() + 1, 0);

            const isBillingDateOfMonth = now.getDate() === billingDateOfMonth.getDate();

            return isBillingDateOfMonth ? billingDateOfMonth : new Date(now.getFullYear(), now.getMonth(), 0);
        }

        getNoteMesssage = () => {
            const billingDate = this.lastBillingDate;
            const month = billingDate.getMonth();
            const monthName = monthNames[month];

            return `The latest billing date is <strong>${billingDate.getDate()} ${monthName} ${billingDate.getFullYear()}</strong>. Any changes before this date will not be allowed.`;
        }

        handleChangeFromDate = (momentDate) => {
            this.setState({ fromDate: momentDate.format("MM/DD/YYYY") });
        }

        handleSubmit = async () => {
            const { selectedMemberId } = this.props;
            const { fromDate } = this.state;

            const result = await this.props.updateMember({ memberId: selectedMemberId, fromDate });

            if (result) {
                if (result.IsSuccessful) {
                    this.props.fetchData(this.props.param);

                    window.setTimeout(this.handleClosePopup, 3000);
                }

                this.setState({
                    isSuccess: result.IsSuccessful,
                    isShowMessage: true,
                    message: result.IsSuccessful ? "The selected member has been updated successfully!" : result.Message
                }, () => window.setTimeout(this.hideMessage, 3000));
            } else {
                this.setState({
                    isSuccess: false,
                    isShowMessage: true,
                    message: "Updating the selected member is failed!"
                }, () => window.setTimeout(this.hideMessage, 3000));
            }
        }

        handleClosePopup = () => {
            this.props.setSelectedMemberId(null);
            this.setState(initialState);
        }

        hideMessage = () => {
            this.setState({
                isShowMessage: false
            });
        }

        render() {
            const { selectedMemberId } = this.props;
            const { memberName, affUserName, isShowMessage, isSuccess, message, fromDate } = this.state;

            const isShowPopup = !!selectedMemberId;
            const noteMessage = this.getNoteMesssage();

            return (
                <Modal isOpen={isShowPopup} toggle={this.handleClosePopup} backdrop='static'>
                    <ModalHeader toggle={this.handleClosePopup}><span className="iconpopup"></span>Remove Player {memberName}</ModalHeader>
                    <ModalBody>
                        <div className="modal-body-wrapper">
                            <div className="edit-member-form-container">
                                <Alert color={isSuccess ? 'success' : 'danger'} isOpen={isShowMessage}>
                                    <div dangerouslySetInnerHTML={{ __html: message }}></div>
                                </Alert>
                                <Form tag="form" className="mb-3" autoComplete="off">
                                    <FormGroup className="my-2">
                                        <strong className="font-italic">Remove Player '{memberName}' from {affUserName}</strong>
                                    </FormGroup>

                                    <FormGroup row className="mb-2 align-items-center">
                                        <Label htmlFor="fromDate" sm={3} className="required">From Date:</Label>
                                        <Col sm={5}>
                                            <DatePicker 
                                                minDate={this.lastBillingDateString} 
                                                maxDate={this.nextMonthString} 
                                                date={fromDate} 
                                                iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                                                onChange={this.handleChangeFromDate} 
                                            />
                                        </Col>
                                    </FormGroup>

                                    <FormGroup>
                                        <Label className="font-italic" dangerouslySetInnerHTML={{ __html: noteMessage }}></Label>
                                    </FormGroup>
                                </Form>

                                <Button type="button" color="success" className="mr-3" onClick={this.handleSubmit}>{globalResource.Save}</Button>
                                <Button type="button" color="info" onClick={this.handleClosePopup}>{globalResource.Cancel}</Button>
                            </div>
                        </div>
                    </ModalBody>
                </Modal>
            );
        }
    }

    function mapStateToProps({ selectedMemberId, today, param }) {
        return { selectedMemberId, today, param };
    }

    function mapDispatchToProps(dispatch) {
        return bindActionCreators({ fetchData, getMemberDetail, updateMember, setSelectedMemberId }, dispatch);
    }

    export default connect(mapStateToProps, mapDispatchToProps)(EditMemberPopup);