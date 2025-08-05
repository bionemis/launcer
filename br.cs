using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using UnityEngine;
using d;

public class br
{
	private MySqlConnection fl;

	private MySqlCommand fm;

	private string fn;

	public void lk()
	{
		try
		{
			if (fl != null)
			{
				fl.Close();
			}
			fn = "server=127.0.0.1;port=3306;user=root;password=5201314; database=quickflashing;";
			fl = new MySqlConnection(fn);
			fl.Open();
		}
		catch (Exception ex)
		{
			k.ca("SqlHandler.Init():" + ex.Message);
		}
	}

	public void Close()
	{
		try
		{
			fl.Close();
			fl = null;
		}
		catch (Exception ex)
		{
			k.ca("Sql=>HAN:" + ex.Message);
		}
	}

	private void ll()
	{
		mm(bt.LanguageType, "0");
		mm(bt.IsInsertCoins, "False");
		mm(bt.CoinNum, "0");
		mm(bt.VideoVolume, "30");
		mm(bt.IsActiveFixedTime, "True");
		mm(bt.FixedTime, "5");
		mm(bt.IsPlayPublicityVideo, "False");
		mm(bt.IsPlayContentVideo, "True");
	}

	private bool lm(string a)
	{
		fm = new MySqlCommand(a, fl);
		try
		{
			fm.ExecuteNonQuery();
			return true;
		}
		catch (Exception ex)
		{
			Debug.Log("UpdataSqlData:" + ex.Message);
			return false;
		}
	}

	private bool ln(string a)
	{
		fm = new MySqlCommand(a, fl);
		try
		{
			fm.ExecuteNonQuery();
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	private void lo(MySqlCommand a)
	{
		MySqlDataReader mySqlDataReader = a.ExecuteReader();
		try
		{
			while (mySqlDataReader.Read())
			{
				if (mySqlDataReader.HasRows)
				{
					Console.WriteLine("id:" + mySqlDataReader.GetInt32(1) + "    name:" + mySqlDataReader.GetString(3));
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		finally
		{
			mySqlDataReader.Close();
		}
	}

	public List<string> lp()
	{
		List<string> list = new List<string>();
		string cmdText = "select " + by.AccountPsw + " from " + by.Name;
		fm = new MySqlCommand(cmdText, fl);
		MySqlDataReader mySqlDataReader = fm.ExecuteReader();
		try
		{
			while (mySqlDataReader.Read())
			{
				list.Add(mySqlDataReader.GetString(0));
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		finally
		{
			mySqlDataReader.Close();
		}
		return list;
	}

	public void lq(bool a, string b)
	{
		string text = ((!a) ? ("update " + by.Name + " set " + by.AccountPsw + "='" + b + "' where " + by.AccountPower + "='Employee'") : ("update " + by.Name + " set " + by.AccountPsw + "='" + b + "' where " + by.AccountPower + "='Admin'"));
		lm(text);
	}

	private bool lr(string a)
	{
		fm = new MySqlCommand(a, fl);
		try
		{
			fm.ExecuteNonQuery();
			return true;
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
			return false;
		}
	}

	public List<bo> ls(string a, string b)
	{
		List<bo> list = new List<bo>();
		string cmdText = "select " + bw.SqlName + "," + bw.CNName + "," + bw.ENName + "," + bw.PlayTime + "," + bw.PlayerCount + "," + bw.Income + "," + bw.CreateTime + " from " + bw.Name + " where " + bw.CreateTime + " >= '" + a + "' and " + bw.CreateTime + " <='" + b + "'";
		fm = new MySqlCommand(cmdText, fl);
		MySqlDataReader mySqlDataReader = fm.ExecuteReader();
		try
		{
			while (mySqlDataReader.Read())
			{
				bo bo2 = new bo();
				bo2.SqlName = mySqlDataReader.GetString(0);
				bo2.CNName = mySqlDataReader.GetString(1);
				bo2.ENName = mySqlDataReader.GetString(2);
				bo2.PlayTime = mySqlDataReader.GetInt32(3);
				bo2.PlayerCount = mySqlDataReader.GetInt32(4);
				bo2.Income = mySqlDataReader.GetInt32(5);
				bo2.CreateTime = mySqlDataReader.GetDateTime(6);
				list.Add(bo2);
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		finally
		{
			mySqlDataReader.Close();
		}
		return list;
	}

	public bool lt(string a, string b, int c)
	{
		DateTime now = DateTime.Now;
		mh(a, out var num, out var num2);
		string text = lv(now);
		string text2 = $"insert into {bw.Name} ({bw.SqlName},{bw.CNName},{bw.ENName},{bw.PlayTime},{bw.CreateTime}) values('{a}','{b}','{c}','{text}')";
		if (!lr(text2))
		{
			Debug.Log("写入游戏被玩记录失败");
			return false;
		}
		num++;
		num2 += c;
		string text3 = $"update {bu.Name} set {bu.PlayCount}={num}   where {bu.SqlName}=\"{a}\"";
		lm(text3);
		return true;
	}

	public bool lu(bo a)
	{
		string text = "insert into " + bw.Name + " (" + bw.SqlName + "," + bw.CNName + "," + bw.ENName + "," + bw.PlayTime + "," + bw.PlayerCount + "," + bw.Income + "," + bw.CreateTime + ") " + $"values('{a.SqlName}','{a.CNName}','{a.ENName}','{a.PlayTime}','{a.PlayerCount}','" + $"{a.Income}','{lv(a.CreateTime)}')";
		if (!lr(text))
		{
			Debug.Log("写入游戏被玩记录失败");
			return false;
		}
		return true;
	}

	public string lv(DateTime a)
	{
		return a.Year + "/" + a.Month + "/" + a.Day + " " + a.Hour + ":" + a.Minute + ":" + a.Second;
	}

	public string lw(DateTime a)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (a.Day - 3 < 1)
		{
			if (a.Month - 1 < 1)
			{
				num3 = a.Year - 1;
				num2 = 12;
				num = 27;
			}
			else
			{
				num3 = a.Year;
				num2 = a.Month - 1;
				num = 27;
			}
		}
		else
		{
			num3 = a.Year;
			num2 = a.Month;
			num = a.Day - 3;
		}
		return num3 + "/" + num2 + "/" + num + " " + a.Hour + ":" + a.Minute + ":" + a.Second;
	}

	public List<bn> lx(bp a)
	{
		if (fl != null && fl.State != ConnectionState.Open)
		{
			return null;
		}
		List<bn> list = new List<bn>();
		string text = "select " + bu.SqlName + "," + bu.PlayCount + "," + bu.ContentPattern + "," + bu.ContentType + "," + bu.StarNum + "," + bu.IsShow + "," + bu.CreateTime + " from " + bu.Name;
		switch (a)
		{
		case bp.All:
			text = text + " ORDER BY " + bu.ContentPattern + " ASC, " + bu.StarNum + " DESC," + bu.PlayCount + " DESC";
			break;
		case bp.Hot:
			text = text + " ORDER BY " + bu.PlayCount + " DESC," + bu.StarNum + " DESC," + bu.ContentPattern + " ASC";
			break;
		case bp.Children:
			text = text + " where " + bu.ContentType + "=10 ORDER BY " + bu.ContentPattern + " ASC," + bu.StarNum + " DESC, " + bu.PlayCount + " DESC";
			break;
		case bp.Horrible:
			text = text + " where " + bu.ContentType + "=20 ORDER BY " + bu.ContentPattern + " ASC," + bu.StarNum + " DESC, " + bu.PlayCount + " DESC";
			break;
		case bp.Adventure:
			text = text + " where " + bu.ContentType + "=30 ORDER BY " + bu.ContentPattern + " ASC," + bu.StarNum + " DESC, " + bu.PlayCount + " DESC";
			break;
		}
		fm = new MySqlCommand(text, fl);
		MySqlDataReader mySqlDataReader = fm?.ExecuteReader();
		try
		{
			while (mySqlDataReader.Read())
			{
				if (mySqlDataReader.HasRows)
				{
					string text2 = mySqlDataReader.GetString(0);
					int @int = mySqlDataReader.GetInt32(1);
					ContentPattern int2 = (ContentPattern)mySqlDataReader.GetInt32(2);
					ContentType int3 = (ContentType)mySqlDataReader.GetInt32(3);
					int int4 = mySqlDataReader.GetInt32(4);
					bool boolean = mySqlDataReader.GetBoolean(5);
					DateTime dateTime = mySqlDataReader.GetDateTime(6);
					bn item = new bn(text2, @int, int2, int3, int4, boolean, dateTime);
					list.Add(item);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		mySqlDataReader.Close();
		return list;
	}

	public List<bn> ly()
	{
		if (fl.State != ConnectionState.Open)
		{
			return null;
		}
		List<bn> list = new List<bn>();
		string cmdText = "select " + bu.SqlName + "," + bu.PlayCount + "," + bu.ContentPattern + "," + bu.ContentType + "," + bu.StarNum + "," + bu.IsShow + "," + bu.CreateTime + " from " + bu.Name + " ORDER BY " + bu.StarNum + " DESC";
		fm = new MySqlCommand(cmdText, fl);
		MySqlDataReader mySqlDataReader = fm?.ExecuteReader();
		try
		{
			while (mySqlDataReader.Read())
			{
				if (mySqlDataReader.HasRows)
				{
					string text = mySqlDataReader.GetString(0);
					int @int = mySqlDataReader.GetInt32(1);
					ContentPattern int2 = (ContentPattern)mySqlDataReader.GetInt32(2);
					ContentType int3 = (ContentType)mySqlDataReader.GetInt32(3);
					int int4 = mySqlDataReader.GetInt32(4);
					bool boolean = mySqlDataReader.GetBoolean(5);
					DateTime dateTime = mySqlDataReader.GetDateTime(6);
					bn item = new bn(text, @int, int2, int3, int4, boolean, dateTime);
					list.Add(item);
				}
			}
		}
		catch (Exception)
		{
		}
		mySqlDataReader.Close();
		return list;
	}

	public bool lz(string a, ContentPattern b, ContentType c, int d, bool e)
	{
		string cmdText = "select * from " + bu.Name + " where " + bu.SqlName + " = '" + a + "'";
		fm = new MySqlCommand(cmdText, fl);
		MySqlDataReader mySqlDataReader = fm.ExecuteReader();
		try
		{
			if (mySqlDataReader.Read())
			{
				return false;
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		finally
		{
			mySqlDataReader.Close();
		}
		DateTime now = DateTime.Now;
		string text = lv(now);
		string text2 = "insert into " + bu.Name + " (" + bu.SqlName + "," + bu.PlayCount + "," + bu.ContentPattern + "," + bu.ContentType + "," + bu.StarNum + "," + bu.IsShow + "," + bu.CreateTime + ") " + $"values('{a}','{0}','{(int)b}','{(int)c}','{d}','{e}','{text}')";
		if (!lr(text2))
		{
			return false;
		}
		return true;
	}

	public bool ma(string a)
	{
		string cmdText = "delete from " + bu.Name + " where " + bu.SqlName + " = '" + a + "'";
		try
		{
			fm = new MySqlCommand(cmdText, fl);
			fm.ExecuteReader().Close();
			return true;
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
			return false;
		}
	}

	public bool mb(bn a)
	{
		string text = "update " + bu.Name + " set " + $"{bu.PlayCount}='{a.PlayCount}'," + $"{bu.ContentPattern}='{(int)a.ContentPattern}'," + $"{bu.ContentType}='{(int)a.ContentType}'," + $"{bu.StarNum}='{a.StarNum}'," + $"{bu.IsShow}='{a.IsShow}' " + "where " + bu.SqlName + " = '" + fn + "'";
		return lm(text);
	}

	public bool mc(string a, ContentPattern b, ContentType c, int d, bool e)
	{
		string text = "update " + bu.Name + " set " + $"{bu.ContentPattern}='{(int)b}'," + $"{bu.ContentType}='{(int)c}'," + $"{bu.StarNum}='{d}'," + $"{bu.IsShow}='{e}'" + " where " + bu.SqlName + " = '" + a + "'";
		return lm(text);
	}

	public bool md(string a, int b)
	{
		string text = $"update {bu.Name} set {bu.PlayCount}='{b}' where {bu.SqlName} = '{a}'";
		return lm(text);
	}

	public List<string> me(bp a)
	{
		List<string> result = new List<string>();
		string text = string.Empty;
		switch (a)
		{
		case bp.All:
			text = "select " + bu.SqlName + " from " + bu.Name + " where " + bu.IsShow + "='TRUE' ORDER BY " + bu.StarNum + " DESC, " + bu.ContentPattern + " ASC";
			break;
		case bp.Hot:
			text = "select " + bu.SqlName + " from " + bu.Name + " where " + bu.IsShow + "='TRUE' ORDER BY " + bu.PlayCount + " DESC";
			break;
		case bp.Children:
			text = "select " + bu.SqlName + " from " + bu.Name + " where " + bu.IsShow + "='TRUE' AND " + bu.ContentType + "=10 ORDER BY " + bu.StarNum + " DESC, " + bu.PlayCount + " DESC";
			break;
		case bp.Horrible:
			text = "select " + bu.SqlName + " from " + bu.Name + " where " + bu.IsShow + "='TRUE' AND " + bu.ContentType + "=20 ORDER BY " + bu.StarNum + " DESC, " + bu.PlayCount + " DESC";
			break;
		case bp.Adventure:
			text = "select " + bu.SqlName + " from " + bu.Name + " where " + bu.IsShow + "='TRUE' AND " + bu.ContentType + "=30 ORDER BY " + bu.StarNum + " DESC, " + bu.PlayCount + " DESC";
			break;
		}
		mf(text, out result);
		return result;
	}

	private void mf(string a, out List<string> b)
	{
		List<string> list = new List<string>();
		fm = new MySqlCommand(a, fl);
		MySqlDataReader mySqlDataReader = fm.ExecuteReader();
		try
		{
			while (mySqlDataReader.Read())
			{
				if (mySqlDataReader.HasRows)
				{
					list.Add(mySqlDataReader.GetString(0));
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		finally
		{
			mySqlDataReader.Close();
		}
		b = list;
	}

	public int mg()
	{
		int num = 0;
		string cmdText = "select * from " + bu.Name;
		fm = new MySqlCommand(cmdText, fl);
		MySqlDataReader mySqlDataReader = fm.ExecuteReader();
		try
		{
			while (mySqlDataReader.Read())
			{
				if (mySqlDataReader.HasRows)
				{
					num += mySqlDataReader.GetInt32(2);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		finally
		{
			mySqlDataReader.Close();
		}
		return num;
	}

	private void mh(string a, out int b, out int c)
	{
		b = 0;
		c = 0;
		string cmdText = $"select * from game_content where GameName=\"{a}\"";
		fm = new MySqlCommand(cmdText, fl);
		MySqlDataReader mySqlDataReader = fm.ExecuteReader();
		try
		{
			while (mySqlDataReader.Read())
			{
				if (mySqlDataReader.HasRows)
				{
					b = mySqlDataReader.GetInt32(2);
					c = mySqlDataReader.GetInt32(3);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		finally
		{
			mySqlDataReader.Close();
		}
	}

	public bool mi()
	{
		try
		{
			ln("delete from " + bw.Name);
		}
		catch (Exception)
		{
			return false;
		}
		return true;
	}

	public void mj(out int a, out int b)
	{
		a = 0;
		b = 0;
		string cmdText = "select " + bv.CoinsNumber + " from " + bv.Name;
		fm = new MySqlCommand(cmdText, fl);
		MySqlDataReader mySqlDataReader = fm.ExecuteReader();
		try
		{
			while (mySqlDataReader.Read())
			{
				if (mySqlDataReader.HasRows)
				{
					a++;
					b += mySqlDataReader.GetInt32(0);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		finally
		{
			mySqlDataReader.Close();
		}
	}

	public string mk(bt a)
	{
		string result = string.Empty;
		string cmdText = "select " + bx.SettingValue + " from " + bx.Name + " where " + bx.SettingName + " = '" + a.ToString() + "'";
		fm = new MySqlCommand(cmdText, fl);
		try
		{
			MySqlDataReader mySqlDataReader = fm.ExecuteReader();
			while (mySqlDataReader.Read())
			{
				if (mySqlDataReader.HasRows)
				{
					result = mySqlDataReader.GetString(0);
				}
			}
			mySqlDataReader.Close();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		return result;
	}

	public void ml(bt a, string b)
	{
		try
		{
			string text = "update " + bx.Name + " set " + bx.SettingValue + "='" + b + "' where " + bx.SettingName + " = '" + a.ToString() + "'";
			lm(text);
		}
		catch (Exception ex)
		{
			k.ca("MainXml:" + ex.Message);
		}
	}

	private void mm(bt a, string b = "1")
	{
		string text = string.Empty;
		string cmdText = $"select {bx.SettingValue} from {bx.Name} where {bx.SettingName} = '{a}'";
		fm = new MySqlCommand(cmdText, fl);
		MySqlDataReader mySqlDataReader = fm.ExecuteReader();
		try
		{
			while (mySqlDataReader.Read())
			{
				if (mySqlDataReader.HasRows)
				{
					text = mySqlDataReader.GetString(0);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
		finally
		{
			mySqlDataReader.Close();
		}
		if (text.Length == 0)
		{
			string text2 = "insert into " + bx.Name + " (" + bx.SettingName + "," + bx.SettingValue + ") values('" + a.ToString() + "','" + b + "')";
			if (!lr(text2))
			{
				Debug.Log("写入setting表失败");
			}
		}
	}
}
