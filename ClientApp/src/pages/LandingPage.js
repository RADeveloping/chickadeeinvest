// create a new component
import React from 'react';

// render the component
function LandingPage() {
    return (
        <>
            <nav className="navbar navbar-expand-lg fixed-top text-dark" id="mainNav">
                <div className="container">
                    <a className="navbar-brand" href="#page-top"><img src="static/landingpage/assets/img/navbar-logo.svg" alt="..."/></a>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse"
                            data-bs-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false"
                            aria-label="Toggle navigation">
                        Menu
                        <i className="fas fa-bars ms-1"></i>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarResponsive">
                        <ul className="navbar-nav text-uppercase ms-auto py-4 py-lg-0">
                            <li className="nav-item"><a className="nav-link" href="#about">About</a></li>
                            <li className="nav-item"><a className="nav-link" href="#services">Services</a></li>
                            <li className="nav-item"><a className="nav-link" href="#team">Team</a></li>
                            <li className="nav-item d-none d-lg-block"><a className="nav-link"> | </a></li>
                            <li className="nav-item"><a className="nav-link" href="/login">Login</a></li>
                            <li className="nav-item"><a className="nav-link" href="/signup">Signup</a></li>
                        </ul>
                    </div>
                </div>
            </nav>
            <header className="masthead">
                <div className="container">
                    <div className="masthead-heading text-uppercase">Chickadee</div>
                    <div className="masthead-subheading"></div>
                    <a className="btn btn-primary btn-xl text-uppercase" href="#about">Learn More</a>
                </div>
            </header>
            <section className="page-section" id="about">
                <div className="container">
                    <div className="text-center">
                        <h2 className="section-heading text-uppercase">About Us</h2>
                        <h3 className="section-subheading text-muted"></h3>
                    </div>
                    <div className="row text-center">
                        <div className="lead">
                            <p>Chickadee is a real estate services company that provides tools for investors, property
                                managers, and tenants when managing a property. Chickadee provides services for tenants
                                and property managers and ensures that payment history is managed and resolves service
                                requests. Chickadee works with investors to ensure that the properties are at a low risk
                                of vacancy ensuring cash flow.</p>
                        </div>
                    </div>
                </div>
            </section>
            <section className="page-section cream" id="services">
                <div className="container">
                    <div className="text-center">
                        <h2 className="section-heading text-uppercase">Services</h2>
                        <h3 className="section-subheading text-muted"></h3>
                    </div>
                    <div className="row text-center">
                        <div className="col-md-4">
                        <span className="fa-stack fa-4x">
                            <i className="fas fa-circle fa-stack-2x text-primary"></i>
                            <i className="fas fa-money-check-dollar fa-stack-1x fa-inverse"></i>
                        </span>
                            <h4 className="my-3">Investors</h4>
                            <p className="text-muted">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Minima
                                maxime quam architecto quo inventore harum ex magni, dicta impedit.</p>
                        </div>
                        <div className="col-md-4">
                        <span className="fa-stack fa-4x">
                            <i className="fas fa-circle fa-stack-2x text-primary"></i>
                            <i className="fas fa-list-check fa-stack-1x fa-inverse"></i>
                        </span>
                            <h4 className="my-3">Property Managers</h4>
                            <p className="text-muted">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Minima
                                maxime quam architecto quo inventore harum ex magni, dicta impedit.</p>
                        </div>
                        <div className="col-md-4">
                        <span className="fa-stack fa-4x">
                            <i className="fas fa-circle fa-stack-2x text-primary"></i>
                            <i className="fas fa-solid fa-people-roof fa-stack-1x fa-inverse"></i>
                        </span>
                            <h4 className="my-3">Tenants</h4>
                            <p className="text-muted">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Minima
                                maxime quam architecto quo inventore harum ex magni, dicta impedit.</p>
                        </div>
                    </div>
                </div>
            </section>
            <section className="page-section" id="team">
                <div className="container">
                    <div className="text-center">
                        <h2 className="section-heading text-uppercase">Our Amazing Team</h2>
                        <h3 className="section-subheading text-muted">The team that makes it all happen.</h3>
                    </div>
                    <div className="row">
                        <div className="col-lg-4">
                            <div className="team-member">
                                <img className="mx-auto rounded-circle" src="static/landingpage/assets/img/team/1.jpg" alt="..."/>
                                <h4>Parveen Anand</h4>
                                <p className="text-muted">Lead Designer</p>
                                <a className="btn btn-dark btn-social mx-2" href="#!"
                                   aria-label="Parveen Anand Twitter Profile"><i className="fab fa-twitter"></i></a>
                                <a className="btn btn-dark btn-social mx-2" href="#!"
                                   aria-label="Parveen Anand Facebook Profile"><i className="fab fa-facebook-f"></i></a>
                                <a className="btn btn-dark btn-social mx-2" href="#!"
                                   aria-label="Parveen Anand LinkedIn Profile"><i
                                    className="fab fa-linkedin-in"></i></a>
                            </div>
                        </div>
                        <div className="col-lg-4">
                            <div className="team-member">
                                <img className="mx-auto rounded-circle" src="static/landingpage/assets/img/team/2.jpg" alt="..."/>
                                <h4>Diana Petersen</h4>
                                <p className="text-muted">Lead Marketer</p>
                                <a className="btn btn-dark btn-social mx-2" href="#!"
                                   aria-label="Diana Petersen Twitter Profile"><i className="fab fa-twitter"></i></a>
                                <a className="btn btn-dark btn-social mx-2" href="#!"
                                   aria-label="Diana Petersen Facebook Profile"><i
                                    className="fab fa-facebook-f"></i></a>
                                <a className="btn btn-dark btn-social mx-2" href="#!"
                                   aria-label="Diana Petersen LinkedIn Profile"><i
                                    className="fab fa-linkedin-in"></i></a>
                            </div>
                        </div>
                        <div className="col-lg-4">
                            <div className="team-member">
                                <img className="mx-auto rounded-circle" src="static/landingpage/assets/img/team/3.jpg" alt="..."/>
                                <h4>Larry Parker</h4>
                                <p className="text-muted">Lead Developer</p>
                                <a className="btn btn-dark btn-social mx-2" href="#!"
                                   aria-label="Larry Parker Twitter Profile"><i className="fab fa-twitter"></i></a>
                                <a className="btn btn-dark btn-social mx-2" href="#!"
                                   aria-label="Larry Parker Facebook Profile"><i className="fab fa-facebook-f"></i></a>
                                <a className="btn btn-dark btn-social mx-2" href="#!"
                                   aria-label="Larry Parker LinkedIn Profile"><i className="fab fa-linkedin-in"></i></a>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-lg-8 mx-auto text-center"><p className="large text-muted"></p></div>
                    </div>
                </div>
            </section>
            <section className="page-section" id="contact">
                <div className="container">
                    <div className="text-center">
                        <h2 className="section-heading text-uppercase">Contact Us</h2>
                        <h3 className="section-subheading text-muted">Lorem ipsum dolor sit amet consectetur.</h3>
                    </div>
                    <form id="contactForm" data-sb-form-api-token="API_TOKEN">
                        <div className="row align-items-stretch mb-5">
                            <div className="col-md-6">
                                <div className="form-group">
                                    <input className="form-control" id="name" type="text" placeholder="Your Name *"
                                           data-sb-validations="required"/>
                                    <div className="invalid-feedback" data-sb-feedback="name:required">A name is
                                        required.
                                    </div>
                                </div>
                                <div className="form-group">
                                    <input className="form-control" id="email" type="email" placeholder="Your Email *"
                                           data-sb-validations="required,email"/>
                                    <div className="invalid-feedback" data-sb-feedback="email:required">An email is
                                        required.
                                    </div>
                                    <div className="invalid-feedback" data-sb-feedback="email:email">Email is not
                                        valid.
                                    </div>
                                </div>
                                <div className="form-group mb-md-0">
                                    <input className="form-control" id="phone" type="tel" placeholder="Your Phone *"
                                           data-sb-validations="required"/>
                                    <div className="invalid-feedback" data-sb-feedback="phone:required">A phone number
                                        is required.
                                    </div>
                                </div>
                            </div>
                            <div className="col-md-6">
                                <div className="form-group form-group-textarea mb-md-0">
                                    <textarea className="form-control" id="message" placeholder="Your Message *"
                                              data-sb-validations="required"></textarea>
                                    <div className="invalid-feedback" data-sb-feedback="message:required">A message is
                                        required.
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="d-none" id="submitSuccessMessage">
                            <div className="text-center text-white mb-3">
                                <div className="fw-bolder">Form submission successful!</div>
                                To activate this form, sign up at
                                <br/>
                                <a href="https://startbootstrap.com/solution/contact-forms">https://startbootstrap.com/solution/contact-forms</a>
                            </div>
                        </div>
                        <div className="d-none" id="submitErrorMessage">
                            <div className="text-center text-danger mb-3">Error sending message!</div>
                        </div>
                        <div className="text-center">
                            <button className="btn btn-primary btn-xl text-uppercase disabled" id="submitButton"
                                    type="submit">Send Message
                            </button>
                        </div>
                    </form>
                </div>
            </section>
            <footer className="footer py-4">
                <div className="container">
                    <div className="row align-items-center">
                        <div className="col-lg-4 text-lg-start">Copyright &copy; Chickadee Investments 2022</div>
                        <div className="col-lg-4 my-3 my-lg-0">
                            <a className="btn btn-dark btn-social mx-2" href="#!" aria-label="Twitter"><i
                                className="fab fa-twitter"></i></a>
                            <a className="btn btn-dark btn-social mx-2" href="#!" aria-label="Facebook"><i
                                className="fab fa-facebook-f"></i></a>
                            <a className="btn btn-dark btn-social mx-2" href="#!" aria-label="LinkedIn"><i
                                className="fab fa-linkedin-in"></i></a>
                        </div>
                        <div className="col-lg-4 text-lg-end">
                            <a className="link-dark text-decoration-none me-3" href="#!">Privacy Policy</a>
                            <a className="link-dark text-decoration-none" href="#!">Terms of Use</a>
                        </div>
                    </div>
                </div>
            </footer>
            <script src="/landingpage/js/scripts.js"></script>
        </>
    );
    }

export default LandingPage;