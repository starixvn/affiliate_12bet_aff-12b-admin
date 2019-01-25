import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Col, Button, Form, FormGroup, Label, Input, FormFeedback, Modal, ModalHeader, ModalBody } from 'reactstrap';
import { setShowPopupEdit } from '../actions';
import { globalResource } from '../../../../modules/globalResource';

export class EditNoteForm extends Component {
    constructor(props) {
        super(props);
    }

    handleChange = (event) => {
        this.setState({ value: event.target.value });
    }

    render() {
        const { isShowPopupEdit, param } = this.props;

        return (
            <Modal isOpen={isShowPopupEdit} toggle={() => this.props.setShowPopupEdit(false)} backdrop='static'>
                <ModalHeader toggle={() => this.props.setShowPopupEdit(false)}><span className="iconpopup"></span>Edit</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        <div className="change-password-form-container">
                            <Form tag="form" className="mb-5" autocomplete="off">
                                <FormGroup row className="mb-2">
                                    <Label for="Content" sm={4} className="required">Content</Label>
                                    <Col sm={5}>
                                        <Input type="text" name="Content" id="Content" value={param.Content} onChange={this.handleChange} />
                                    </Col>
                                </FormGroup>
                            </Form>

                            <Button type="button" color="success" className="mr-3">{globalResource.Submit}</Button>
                            <Button type="button" color="info" onClick={() => this.props.setShowPopupEdit(false)}>{globalResource.Cancel}</Button>
                        </div>
                    </div>
                </ModalBody>
            </Modal>
        );
    }
}

function mapStateToProps({ isShowPopupEdit, param }) {
    return Object.assign({}, { isShowPopupEdit, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopupEdit }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(EditNoteForm);