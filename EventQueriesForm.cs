using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EventVerse
{
    public class EventQueriesForm : BaseForm
    {
        private ComboBox eventFilterComboBox;
        private ListBox queriesListBox;
        private TextBox questionTextBox;
        private TextBox answerTextBox;
        private Button submitAnswerButton;

        private List<EventQuery> queries;

        public EventQueriesForm()
        {
            InitializeComponents();
            SetTitle("Event Queries");
            LoadQueries();
        }

        private void InitializeComponents()
        {
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label titleLabel = new Label
            {
                Text = "Event Queries",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(300, 30)
            };

            Label eventFilterLabel = new Label
            {
                Text = "Filter by Event:",
                Location = new Point(20, 60),
                Size = new Size(100, 20)
            };

            eventFilterComboBox = new ComboBox
            {
                Location = new Point(130, 60),
                Size = new Size(200, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            eventFilterComboBox.SelectedIndexChanged += EventFilterComboBox_SelectedIndexChanged;

            queriesListBox = new ListBox
            {
                Location = new Point(20, 100),
                Size = new Size(750, 200)
            };
            queriesListBox.SelectedIndexChanged += QueriesListBox_SelectedIndexChanged;

            Label questionLabel = new Label
            {
                Text = "Question:",
                Location = new Point(20, 320),
                Size = new Size(100, 20)
            };
            questionTextBox = new TextBox
            {
                Location = new Point(20, 340),
                Size = new Size(750, 60),
                Multiline = true,
                ReadOnly = true
            };

            Label answerLabel = new Label
            {
                Text = "Answer:",
                Location = new Point(20, 420),
                Size = new Size(100, 20)
            };
            answerTextBox = new TextBox
            {
                Location = new Point(20, 440),
                Size = new Size(750, 60),
                Multiline = true
            };

            submitAnswerButton = new Button
            {
                Text = "Submit Answer",
                Location = new Point(670, 520),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(0, 102, 204),
                ForeColor = Color.White
            };
            submitAnswerButton.Click += SubmitAnswerButton_Click;

            this.Controls.AddRange(new Control[]
            {
                titleLabel, eventFilterLabel, eventFilterComboBox, queriesListBox, questionLabel, questionTextBox,
                answerLabel, answerTextBox, submitAnswerButton
            });
        }

        private void LoadQueries()
        {
            // TODO: Implement logic to load queries from the database
            // For now, we'll use placeholder data
            queries = new List<EventQuery>
            {
                new EventQuery { Id = 1, EventName = "Tech Conference 2024", AttendeeName = "Alice", Question = "What time does the event start?", Status = QueryStatus.Pending },
                new EventQuery { Id = 2, EventName = "Music Festival", AttendeeName = "Bob", Question = "Is there parking available?", Status = QueryStatus.Answered, Answer = "Yes, free parking is available on-site." },
                new EventQuery { Id = 3, EventName = "Tech Conference 2024", AttendeeName = "Charlie", Question = "Will there be vegetarian food options?", Status = QueryStatus.Pending },
                new EventQuery { Id = 4, EventName = "Business Workshop", AttendeeName = "David", Question = "Is there a dress code for the event?", Status = QueryStatus.Pending }
            };

            eventFilterComboBox.Items.Add("All Events");
            eventFilterComboBox.Items.AddRange(queries.Select(q => q.EventName).Distinct().ToArray());
            eventFilterComboBox.SelectedIndex = 0;

            UpdateQueriesListBox();
        }

        private void UpdateQueriesListBox()
        {
            queriesListBox.Items.Clear();
            foreach (var query in queries)
            {
                queriesListBox.Items.Add($"{query.EventName} - {query.AttendeeName}: {query.Question} ({query.Status})");
            }
        }

        private void EventFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEvent = eventFilterComboBox.SelectedItem.ToString();

            if (selectedEvent == "All Events")
            {
                UpdateQueriesListBox();
            }
            else
            {
                queriesListBox.Items.Clear();
                foreach (var query in queries.Where(q => q.EventName == selectedEvent))
                {
                    queriesListBox.Items.Add($"{query.EventName} - {query.AttendeeName}: {query.Question} ({query.Status})");
                }
            }
        }

        private void QueriesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (queriesListBox.SelectedIndex != -1)
            {
                var selectedQuery = queries[queriesListBox.SelectedIndex];
                questionTextBox.Text = selectedQuery.Question;
                answerTextBox.Text = selectedQuery.Answer;
            }
        }

        private void SubmitAnswerButton_Click(object sender, EventArgs e)
        {
            if (queriesListBox.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(answerTextBox.Text))
            {
                var selectedQuery = queries[queriesListBox.SelectedIndex];
                selectedQuery.Answer = answerTextBox.Text;
                selectedQuery.Status = QueryStatus.Answered;

                // TODO: Implement logic to update the query in the database

                UpdateQueriesListBox();
                MessageBox.Show("Answer submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a query and provide an answer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class EventQuery
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string AttendeeName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public QueryStatus Status { get; set; }
    }

    public enum QueryStatus
    {
        Pending,
        Answered
    }
}