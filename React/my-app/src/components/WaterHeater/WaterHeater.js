import React from "react";
import { Container, Row, Col } from "react-bootstrap";

function WaterHeater() {
  return (
    <section>
      <Container fluid className="home-section" id="home">
        <Container className="home-content">
          <Row>
            <Col md={7} className="home-header">
            </Col>

            <Col md={5} style={{ paddingBottom: 20 }}>
            </Col>
          </Row>
        </Container>
      </Container>
    </section>
  );
}

export default WaterHeater;
